using TMPro;
using UnityEngine.Events;
using UnityEngine;

// To find out how this leaderboard works, see here: https://www.youtube.com/watch?v=-O7zeq7xMLw
public class LeaderboardButton : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField inputField;
    public UnityEvent<string, int> submitScoreEvent;

    public int playerScore;
    private void Start()
    {
        //playerScore = PlayerPrefs.GetInt("PlayerScore", 0); // The player's score is fetched from the PlayerScore PlayerPref.
        //PlayerPrefs.SetInt("PlayerScore", 0);
    }

    // This function is called by the submit button, for sending the name and score to Leaderboard.cs to submit an entry.  
    public void SubmitScore()
    {
        submitScoreEvent.Invoke(inputField.text, playerScore);
    }
}