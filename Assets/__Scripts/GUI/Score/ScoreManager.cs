using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

/// <summary>
/// Manages the game's scoring system and leaderboard.
/// </summary>
public class ScoreManager : Singleton<ScoreManager>
{
    [SerializeField] TMP_Text nicknameText;
    public ScoreText ScoreText => scoreText;
    [SerializeField] ScoreText scoreText;
    public Leaderboard Leaderboard => leaderboard;
    Leaderboard leaderboard;

    [SerializeField] TMP_Text highscoreText;
    [SerializeField] TMP_Text scoreDisplay;

    float multiplier = 0;
    int multiplierCount = 0;
    HighscoreEntry entry;
    float highscore;

    /// <summary>
    /// Overrides the base Awake method to set up initial configurations for score and leaderboard.
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        DisplayNickname();

        entry = new HighscoreEntry(0, GlobalSettingManager.Instance.GetNickname(), Systems.Instance.difficultyLevel.DifficultyName);
        leaderboard = new Leaderboard();
        leaderboard.Load();

        CreateMockLeaderboardData();

        highscore = leaderboard.GetHighscore(entry);
        highscoreText.gameObject.SetActive(false);
        DisplayScore();
    }

    private void DisplayNickname()
    {
        nicknameText.text = GlobalSettingManager.Instance.GetNickname();
    }

    /// <summary>
    /// Creates mock data for the leaderboard for testing purposes.
    /// </summary>
    private void CreateMockLeaderboardData()
    {
        Random.State originalSeed = Random.state; 

        Random.InitState(123);

        List<string> allNicknames = new List<string> {
        "Beginner Buddy", "Easy Goer", "No Sweat", "Chill Champ", "SimpleSimon",
        "Random Rider", "Moderate Master", "Decent Dabbler", "Average Ace", "Midway Maven",
        "Queen Of Chaos", "Hardcore Hero" };

        List<string> allDifficulties = new List<string> {"Easy", "Medium", "Hard"};

        foreach (var nickname in allNicknames)
        {
            foreach (var difficulty in allDifficulties)
            {
                int score = Random.Range(1, 10000);
                leaderboard.UpdateScore(new HighscoreEntry(score, nickname, difficulty));
            }
        }

        Random.state = originalSeed;
    }

    /// <summary>
    /// Sets the multiplier based on the game's difficulty level.
    /// </summary>
    private void Start()
    {
        multiplier = Systems.Instance.difficultyLevel.MultiplierIncrement;
    }


    /// <summary>
    /// Adds points to the player's score, updates the leaderboard, and displays the score.
    /// </summary>
    /// <param name="points">The points to be added.</param>
    public void AddPoints(float points)
    {
        if (entry.Score + points >= 0)
        {
            entry.Score += points;
        }

        leaderboard.UpdateScore(entry);
        DisplayScore();
    }

    /// <summary>
    /// Displays the current score and manages high score notifications.
    /// </summary>
    void DisplayScore()
    {
        if (highscore != 0 && entry.Score > highscore)
        {
            highscoreText.gameObject.SetActive(true);
        }
        else
        {
            highscoreText.gameObject.SetActive(false);
        }

        scoreDisplay.text = $"Score: {entry.Score} (X{multiplierCount + 1})";
    }

    /// <summary>
    /// Calculates the points based on various conditions such as being negative or critical.
    /// </summary>
    /// <param name="basePoints">The base points to calculate from.</param>
    /// <param name="isNegative">Flag indicating if the points are negative.</param>
    /// <param name="isCritical">Flag indicating if the points are critical.</param>
    /// <returns>The calculated points based on the conditions.</returns>
    public float CalculatePoints(float basePoints, bool isNegative = false, bool isCritical = false)
    {
        basePoints = Mathf.Abs(basePoints);

        if (isNegative)
        {
            float negativePoints = -Mathf.Ceil(basePoints / 2);
            return negativePoints;
        }
        else if (isCritical)
        {
            float criticalPoints = Mathf.Ceil(basePoints * (1 + multiplierCount * multiplier)) * 2;
            return criticalPoints;
        }

        float regularPoints = Mathf.Ceil(basePoints * (1 + multiplierCount * multiplier));
        return regularPoints;
    }

    /// <summary>
    /// Resets the score multiplier to zero and updates the displayed score.
    /// </summary>
    public void ResetMultiplier()
    {
        multiplierCount = 0;
        DisplayScore();
    }

    /// <summary>
    /// Decrements the score multiplier and updates the displayed score.
    /// </summary>
    public void DecrementMultiplier()
    {
        multiplierCount--;
        if (multiplierCount <= 0)
        {
            multiplierCount = 0;
        }
        DisplayScore();
    }

    /// <summary>
    /// Increments the score multiplier.
    /// </summary>
    public void IncrementMultiplier()
    {
        multiplierCount++;
    }

}
