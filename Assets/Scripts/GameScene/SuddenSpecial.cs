using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class SuddenSpecial : MonoBehaviour
{
    private int currentTarget = 0;
    private float maxTargets;
    private Score score;
    public int jackpotValue;

    // Start is called before the first frame update
    void Start()
    {
        maxTargets = transform.childCount;
        SetTargets();

        // In order to access the Score Manager, and make this object a prefab, we must get access to it via this method:
        score = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<Score>();
    }

    // This goes through the list of child Sudden Special Targets, and finds the current target the pinball needs to hit.
    public void SetTargets()
    {
        for (int i = 0; i < maxTargets; i++) 
        {
            if(i == currentTarget)
            {
                transform.GetChild(i).GetComponent<RewardScript>().Activate(true); //
            }
            else
            {
                transform.GetChild(i).GetComponent<RewardScript>().Activate(false);
            }
        }
    }
    
    /* This function is called by RewardScript.cs. If the target is a sudden special target, and it has been collided with
     * while activated. It tells this script to look to the next target. */
    public void NextTarget()
    {
        if(currentTarget == maxTargets-1)
        {
            Debug.Log("Sudden Special Jackpot!");
            score.AddScore(jackpotValue); // Awards jackpot value to current score.
        }
        else
        {
            currentTarget++;
            Debug.Log("Next target: " + currentTarget);
            SetTargets();
        }
    }
}
