using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardScript : MonoBehaviour
{
    private Score score;
    [Header("Value")]
    public int scoreValue;
    [Header("Target Types")]
    public bool isSuddenSpecial = false;
    public bool isDropTarget = false;
    [Header("Drop Target Values")]
    public float moleMin = 2.0f;
    public float moleMax = 5.0f;
    
    private bool disabled = false;

    private void Start()
    {
        // In order to access the Score Manager, and make this object a prefab, we must get access to it via this method:
        score = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<Score>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinball") && !disabled)
        {
            score.AddScore(scoreValue);

            if (isSuddenSpecial)
            {
                transform.parent.GetComponent<SuddenSpecial>().NextTarget();
            }

            // This if-statement contains all the targets which shouldn't destroy itself (using '!')
            if (!isDropTarget && !isSuddenSpecial)
            { 
                KillTarget(); 
            }

            // This if-statement is only for the drop target, and hides the target for a random period of time.
            if (isDropTarget)
            {
                StartCoroutine(hideMole());
            }
        }
    }

    public  IEnumerator hideMole()
    {
        disabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        yield return new WaitForSeconds(Random.Range(moleMin, moleMax));
        disabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    // This function disables and hides the target.
    public void KillTarget()
    {
        disabled = true;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        // Here we can add some kind of death animation, with a cloud particle effect.
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

}
