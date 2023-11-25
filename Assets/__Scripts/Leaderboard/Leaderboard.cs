using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Leaderboard: ISavable
{
    public List<HighscoreEntry> HighscoreEntries;

    public Leaderboard() 
    {
        HighscoreEntries = new List<HighscoreEntry>();
    }

    public void Save()
    {
        SaveManager<Leaderboard>.Save(this, GetDataFileName());
    }

    public void Load()
    {
        Leaderboard loaded = SaveManager<Leaderboard>.Load(GetDataFileName());
        HighscoreEntries = loaded.HighscoreEntries;
    }

    public void UpdateScore(string nickname, float score)
    {
        HighscoreEntry entry = HighscoreEntries.FirstOrDefault(a => a.Nickname == nickname);

        if (entry == null)
        {
            HighscoreEntries.Add(new HighscoreEntry { Nickname = nickname, Score = score });
        }
        else
        {
            if(score > entry.Score)
            {
                entry.Score = score;
            }
        }

        SortHighscores();
        Save();
    }

    private void SortHighscores()
    {
        HighscoreEntries = HighscoreEntries.OrderByDescending(e => e.Score).ToList();
    }

    public string GetDataFileName()
    {
        return "Leaderboard.dat";
    }
}
