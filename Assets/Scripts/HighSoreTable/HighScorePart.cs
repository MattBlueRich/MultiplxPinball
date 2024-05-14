using System;

[Serializable]

public class HighScorePart 
{
    public string PlayerName;

    public int Score;

    public HighScorePart (string name, int Score)
    {
        PlayerName = name;
        this.Score = Score;
    }
}
