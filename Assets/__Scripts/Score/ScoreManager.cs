using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public ScoreText ScoreText => scoreText;
    [SerializeField] ScoreText scoreText;

    float multiplier = 0;
    HighscoreEntry entry;
    Leaderboard leaderboard;
    DifficultyLevel difficultyLevel;

    protected override void Awake()
    {
        base.Awake();
        entry = new HighscoreEntry(0, Systems.Instance.Nickname);
        leaderboard = new Leaderboard();
        leaderboard.Load();
    }

    private void Start()
    {
        difficultyLevel = Systems.Instance.difficultyLevel;
    }

    public void AddPoints(float points)
    {
        entry.Score += CalculatePoints(points);

        leaderboard.UpdateScore(entry);
    }

    public float CalculatePoints(float points, bool negative = false)
    {
        points = Mathf.Abs(points);
        if (negative)
        {
            return -Mathf.Ceil(points / 2);
        }

        return Mathf.Ceil(points + points * multiplier);
    }

    public void ResetMultiplier()
    {
        multiplier = 0;
        Debug.Log("Multiplier has been reset.");
    }

    public void IncrementMultiplier()
    {
        multiplier += difficultyLevel.MultiplierIncrement;
        Debug.Log($"Current multiplier: {multiplier}");
    }
}
