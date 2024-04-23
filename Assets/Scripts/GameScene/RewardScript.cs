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

            KillTarget();
        }
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
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }

}
