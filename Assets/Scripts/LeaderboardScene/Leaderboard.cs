using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Dan.Main;
using UnityEngine.Events;

// To find out how this leaderboard works, see here: https://www.youtube.com/watch?v=-O7zeq7xMLw
public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private List<TextMeshProUGUI> names;
    [SerializeField]
    private List<TextMeshProUGUI> scores;

    [SerializeField]
    private TextMeshProUGUI currentScoreText;

    // This is a personalised key from the itch leaderboard page (https://danqzq.itch.io/leaderboard-creator).
    public string publicLeaderboardKey =
        "8707653029ba4c0fedff61ce1222e1a860d7fc2c86a4c028a353524033e5c203";

    private void Start()
    {
        GetLeaderboard();

        currentScoreText.text = "Your Score: " + PlayerPrefs.GetInt("PlayerScore", 0).ToString("0000000000");
    }

    // This function looks online for leaderboard values and writes all values to the names and scores list.
    public void GetLeaderboard()
    {
        // Returns and sets leaderboard entries values from online leaderboard.
        LeaderboardCreator.GetLeaderboard(publicLeaderboardKey, msg => {
            
            // Bugfix: If the length of the leaderboard is less than there are text, use the leaderboard length.
            int loopLength = (msg.Length < names.Count) ? msg.Length : names.Count;
            
            for(int i = 0; i < loopLength; i++) 
            {
                names[i].text = msg[i].Username;
                scores[i].text = msg[i].Score.ToString("0000000000");
            }
        });
    }

    // This function is used for adding a new entry to the online leaderboard, and then updates the values in the game.
    public void SetLeaderboardEntry(string username, int score)
    {
        // Adds new entry to online leaderboard.
        LeaderboardCreator.UploadNewEntry(publicLeaderboardKey, username, score, msg =>
        {
            // Updates game leaderboard.
            GetLeaderboard();
        });

        LeaderboardCreator.ResetPlayer();
    }
}
