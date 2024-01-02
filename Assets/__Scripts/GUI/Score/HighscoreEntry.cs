using System;

/// <summary>
/// Represents an entry in a high score list with score, player nickname, and difficulty name.
/// </summary>
[Serializable]
public class HighscoreEntry
{
    /// <summary>
    /// Initializes a new instance of the HighscoreEntry class with the provided score, nickname, and difficulty name.
    /// </summary>
    /// <param name="score">The score achieved by the player.</param>
    /// <param name="nickname">The player's chosen nickname or username.</param>
    /// <param name="difficultyName">The name of the difficulty level.</param>
    public HighscoreEntry(float score, string nickname, string difficultyName)
    {
        Score = score;
        Nickname = nickname;
        DifficultyName = difficultyName;
    }

    /// <summary>
    /// The score achieved by the player.
    /// </summary>
    public float Score;

    /// <summary>
    /// The player's chosen nickname or username.
    /// </summary>
    public string Nickname;

    /// <summary>
    /// The name of the difficulty level.
    /// </summary>
    public string DifficultyName;
}
