using System;
using System.Collections.Generic;
using System.Linq;

[Serializable]
public class Leaderboard: ISaveable
{
    public string gówno = "xd";
    public int cipa = 69;
    public List<HighscoreEntry> HighscoreEntries;

    public string SaveDataFileName => "Leaderboard.dat";

    public Leaderboard() 
    {
        HighscoreEntries = new List<HighscoreEntry>();
        //Load();
    }

    public void Save()
    {
        SaveManager<Leaderboard>.Save(this, SaveDataFileName);
    }

    public void Load()
    {
        Leaderboard loaded = SaveManager<Leaderboard>.Load(SaveDataFileName);
        HighscoreEntries = loaded.HighscoreEntries;
    }

    public void UpdateScore(string nickname, int score)
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
}

public interface ISaveable
{
    string SaveDataFileName { get; }
    void Save();
    void Load(); 
}