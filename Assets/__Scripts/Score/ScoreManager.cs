using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public ScoreText ScoreText => scoreText;
    [SerializeField] ScoreText scoreText;

    [SerializeField] TMP_Text scoreDisplay;

    float multiplier = 0;
    int multiplierCount = 0;

    HighscoreEntry entry;
    Leaderboard leaderboard;
    DifficultyLevel difficultyLevel;

    protected override void Awake()
    {
        base.Awake();
        entry = new HighscoreEntry(0, Systems.Instance.Nickname);
        leaderboard = new Leaderboard();
        leaderboard.Load();
        DisplayScore();
    }

    private void Start()
    {
        multiplier = Systems.Instance.difficultyLevel.MultiplierIncrement;
    }

    public void AddPoints(float points)
    {
        entry.Score += CalculatePoints(points);

        leaderboard.UpdateScore(entry);
        DisplayScore();
    }

    void DisplayScore()
    {
        scoreDisplay.text = $"Score: {entry.Score} (X{multiplierCount + 1})";
    }

    public float CalculatePoints(float points, bool negative = false)
    {
        points = Mathf.Abs(points);
        if (negative)
        {
            return -Mathf.Ceil(points / 2);
        }

        return Mathf.Ceil(points + points * multiplier * multiplierCount);
    }

    public void ResetMultiplier()
    {
        multiplierCount = 0;
        Debug.Log("Multiplier has been reset.");
    }

    public void IncrementMultiplier()
    {
        multiplierCount++;
        Debug.Log($"Current multiplier: {multiplier*multiplierCount}");
    }
}
