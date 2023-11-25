using System;

[Serializable]
public class HighscoreEntry
{
    public HighscoreEntry(float score, string nickname)
    {
        this.Score = score;
        this.Nickname = nickname;
    }

    public float Score;
    public string Nickname;
}
