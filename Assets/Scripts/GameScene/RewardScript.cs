using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum targetType
{
    Bumper,
    StationaryTarget,
    DropTarget,
    SkillShotTarget,
    SuddenSpecialTarget,
    JackpotLetter
}

public class RewardScript : MonoBehaviour
{
    [SerializeField]
    targetType type;  
        
    [Header("Value")]
    public int scoreValue;
    private Score score;
    public AudioClip[] rewardSFX;

    [Header("Drop Target Values")]
    public float moleMin = 2.0f;
    public float moleMax = 5.0f;

    [Header("Jackpot Letter Sprites")]
    public List<Sprite> jackpotLetterSprites = new List<Sprite>();
    private char jackpotLetter;

    [Header("Explosion Effect")]
    public Material explosionMaterial;
    private bool animateExplosion = false;
    private float explosionFlickerTime = .01f;
    public AudioClip[] explosionSFX;

    [Header("Bumper Properties")]
    public float bumperForce;

    private AudioSource audioSource;
    
    private Object explosionRef;
    private bool disabled = false;  
    private void Start()
    {
        // In order to access the Score Manager, and make this object a prefab, we must get access to it via this method:
        score = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<Score>();

        // If the reward is a jackpot letter, then pick a random letter sprite from the available letters.
        if(type == targetType.JackpotLetter)
        {
            PickRandomLetter();
        }

        explosionRef = Resources.Load("SimpleExplosion");

        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinball") && !disabled)
        {
            score.AddScore(scoreValue);
            PlayRandomSFX("score");

            switch (type)
            {
                case targetType.SuddenSpecialTarget:
                    PlayExplosion();
                    transform.parent.GetComponent<SuddenSpecial>().NextTarget();
                    break;

                case targetType.DropTarget:
                   
                    Vector2 direction = (this.transform.position - collision.transform.position).normalized;
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(direction * -bumperForce, ForceMode2D.Impulse);

                    StartCoroutine(hideMole());
                    break;

                case targetType.StationaryTarget:
                    KillTarget();
                    break;

                case targetType.SkillShotTarget:
                    KillTarget();
                    break;
                case targetType.JackpotLetter:
                    score.AddLetter(jackpotLetter);
                    KillTarget();
                    break;

                case targetType.Bumper:
                    Vector2 bumperDirection = (this.transform.position - collision.transform.position).normalized;
                    collision.gameObject.GetComponent<Rigidbody2D>().AddForce(bumperDirection * -bumperForce, ForceMode2D.Impulse);
                    PlayExplosion();
                    StartCoroutine(PlayBumperAnimation());
                    break;
            }
        }
    }
    public IEnumerator hideMole()
    {
        PlayExplosion();
        disabled = true;
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Hit");
        yield return new WaitForSeconds(Random.Range(moleMin, moleMax));
        disabled = false;
        transform.GetChild(0).GetComponent<Animator>().SetTrigger("Reveal");
    }

    // This function disables and hides the target.
    public void KillTarget()
    {
        disabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        PlayExplosion();
    }
    
    /* This function is called by the SuddenSpecial.cs, which is a empty parent of Sudden Special Targets.
     * When activated, the target can be collided and points can be scored. 
     * When unactivated, the target can't be collided with and has to wait till activated by SuddenSpecial.cs. */
    public void Activate(bool state)
    {
        if (state) 
        {
            disabled = false;
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else
        {
            disabled = true;
            Color32 disabledColour = new Color32(255, 255, 255, 30);
            gameObject.GetComponent<SpriteRenderer>().color = disabledColour;
        }
    }
    public void PickRandomLetter()
    {
        // Pick a random character from the remaining jackpot letters.
        int letterIndex = Random.Range(0, score.jackpotLettersRemaining.Count);
        jackpotLetter = score.jackpotLettersRemaining[letterIndex];

        // Find sprite in list with the same character.
        foreach(Sprite s in jackpotLetterSprites)
        {
            if (s.name.Contains(jackpotLetter))
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = s;
            }
            else
            {
                continue;
            }
        }
    }
    private void Update()
    {
        // This if-statement just animates the explosion particle effect to flicker between colours while active.
        if (animateExplosion)
        {
            explosionFlickerTime -= Time.deltaTime;

            if(explosionFlickerTime < 0)
            {                
                if(explosionMaterial.color == Color.red)
                {
                    explosionMaterial.color = Color.yellow;
                }
                else
                {
                    explosionMaterial.color = Color.red;
                }

                explosionFlickerTime = .01f;
            }
        }
    }
    public void PlayRandomSFX(string soundType)
    {
        switch (soundType)
        {
            case "score":
                audioSource.PlayOneShot(rewardSFX[Random.Range(0, rewardSFX.Length)]);
                break;
            case "explosion":
                audioSource.PlayOneShot(explosionSFX[Random.Range(0, explosionSFX.Length)], 1f);
                break;
            default:
                break;
        }
    }
    public void PlayExplosion()
    {
        // This is all in charge of creating the explosion particle effect.
        GameObject explosion = (GameObject)Instantiate(explosionRef);
        explosion.transform.position = transform.position;
        PlayRandomSFX("explosion");
        StartCoroutine(ExplosionLifeTime());
        IEnumerator ExplosionLifeTime()
        {
            animateExplosion = true;
            yield return new WaitForSeconds(2);
            animateExplosion = false;
            Destroy(explosion);
        }
    }

    IEnumerator PlayBumperAnimation()
    {
        GetComponent<Animator>().SetBool("Angry", true);
        transform.GetChild(0).GetComponent<Animator>().SetBool("Start", true);
        GetComponent<CircleCollider2D>().radius = 2f;
        yield return new WaitForSeconds(.5f);
        GetComponent<Animator>().SetBool("Angry", false);
        transform.GetChild(0).GetComponent<Animator>().SetBool("Start", false);
        GetComponent<CircleCollider2D>().radius = 1f;
    }
}
