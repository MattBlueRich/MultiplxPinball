using System;

[Serializable]

public class HighScorePart 
{
    public string PlayerName;

    public int points;

    public HighScorePart (string name, int points)
    {
        PlayerName = name;
        this.points = points;
    }
}
