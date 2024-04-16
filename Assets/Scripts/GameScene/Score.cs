using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public int score;

    private void Start()
    {
        score = 0; // Subject to change with a lives system.

        //AddScore(500);
    }

    public void AddScore(int scoreValue)
    {
        score += scoreValue;
        scoreText.text = score.ToString();
    }
}
