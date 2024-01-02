using System;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// Represents a leaderboard that manages high score entries and provides methods to update, retrieve, and save scores.
/// </summary>
[Serializable]
public class Leaderboard : ISavable
{
    /// <summary>
    /// The list of high score entries.
    /// </summary>
    public List<HighscoreEntry> HighscoreEntries;

    /// <summary>
    /// Initializes a new instance of the Leaderboard class with an empty list of high score entries.
    /// </summary>
    public Leaderboard()
    {
        HighscoreEntries = new List<HighscoreEntry>();
    }

    /// <summary>
    /// Saves the leaderboard data.
    /// </summary>
    public void Save()
    {
        SaveManager<Leaderboard>.Save(this, GetDataFileName());
    }

    /// <summary>
    /// Loads the leaderboard data.
    /// </summary>
    public void Load()
    {
        Leaderboard loaded = SaveManager<Leaderboard>.Load(GetDataFileName());
        HighscoreEntries = loaded.HighscoreEntries;
        Save();
    }

    /// <summary>
    /// Retrieves the high score of a given entry.
    /// </summary>
    /// <param name="entry">The high score entry to retrieve the score for.</param>
    /// <returns>The high score for the given entry.</returns>
    public float GetHighscore(HighscoreEntry entry)
    {
        HighscoreEntry foundEntry = HighscoreEntries.FirstOrDefault(item =>
            entry.Nickname == item.Nickname &&
            entry.DifficultyName == item.DifficultyName);

        return foundEntry?.Score ?? 0;
    }

    /// <summary>
    /// Updates the high score entry in the leaderboard or adds a new entry if it doesn't exist.
    /// </summary>
    /// <param name="entry">The high score entry to update or add.</param>
    public void UpdateScore(HighscoreEntry entry)
    {
        HighscoreEntry foundEntry = HighscoreEntries.FirstOrDefault(item =>
            entry.Nickname == item.Nickname &&
            entry.DifficultyName == item.DifficultyName);

        if (foundEntry == null)
        {
            HighscoreEntries.Add(entry);
        }
        else if (entry.Score > foundEntry.Score)
        {
            foundEntry.Score = entry.Score;
        }

        SortHighscores();
        Save();
    }

    /// <summary>
    /// Sorts the high score entries by score in descending order.
    /// </summary>
    private void SortHighscores()
    {
        HighscoreEntries = HighscoreEntries.OrderByDescending(e => e.Score).ToList();
    }

    /// <summary>
    /// Retrieves the data file name used for saving and loading.
    /// </summary>
    /// <returns>The data file name.</returns>
    public string GetDataFileName()
    {
        return "Leaderboard.dat";
    }
}
