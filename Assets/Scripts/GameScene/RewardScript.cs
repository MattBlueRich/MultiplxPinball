using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardScript : MonoBehaviour
{
    private Score score;
    public int scoreValue;

    private void Start()
    {
        // In order to access the Score Manager, and make this object a prefab, we must get access to it via this method:
        score = GameObject.FindGameObjectWithTag("ScoreManager").GetComponent<Score>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Pinball"))
        {
            score.AddScore(scoreValue);
            Destroy(this.gameObject);

        }
    }
}
