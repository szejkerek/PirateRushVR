using System;

[Serializable]
public class HighscoreEntry
{
    public HighscoreEntry(float score, string nickname, string difficultyName)
    {
        Score = score;
        Nickname = nickname;
        DifficultyName = difficultyName;
    }

    public float Score;
    public string Nickname;
    public string DifficultyName;
}
