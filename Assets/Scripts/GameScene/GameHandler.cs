using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    //[SerializeField] PointCounter pointCounter;
    [SerializeField] HighScoreHandler HighScoreHandler;
    //[SerializeField] PointHUD pointHUD;
    [SerializeField] string playerName;
    // Start is called before the first frame update
    void StartGame()
    {
        
    }

    // Update is called once per frame
    void StopGame()
    {
        HighScoreHandler.AddHighScoreIfPossible(new HighScorePart(playerName, score.Points));
        
    }
}
