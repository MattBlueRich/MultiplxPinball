using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreHandler : MonoBehaviour
{
    List<HighScorePart> highscoreList = new List<HighScorePart>();
    [SerializeField] int MaxCount = 10;
    [SerializeField] string filename;

    public delegate void OnHighScoreListChanged(List<HighScorePart> list);
    public static event OnHighScoreListChanged onHighScoreListChanged;

    private void Start()
    {
        LoadHighScores();
    }
    private void LoadHighScores()
    {
        highscoreList = FileHandler.ReadListFromJSON<HighScorePart>(filename);

        while (highscoreList.Count > MaxCount)
        {
            hiscoreList.RemoveAt(MaxCount);
        }

        if (onHighScoreListChanged != null)
        {
            onHighScoreListChanged.Invoke(highscoreList);
        }
    }

    private void SaveHighScores()
    {
        FileHandler.SaveToJSON<HighScorePart>(highscoreList, filename);
    }

    public void AddHighScoreIfPossible(HighScorePart ParentHighScore)
    {
        for (int i = 0; i < MaxCount; ++i)
        {
            if (i >= highscoreList.Count || HighScorePart.points > highscoreList[i].points)
            {
                highscoreList.Insert(i, HighSCorePart);

                while (highscoreList.Count > MaxCount)
                {
                    highscoreList.RemoveAt(MaxCount);
                }

                SaveHighScores();

                if (onHighScoreListChanged != null)
                {
                    onHighScoreListChanged.Invoke(highscoreList);
                } 

                break;
            }
        }
    }
        
        
        
}
