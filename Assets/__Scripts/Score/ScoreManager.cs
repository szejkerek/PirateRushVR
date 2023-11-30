using TMPro;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
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
    protected override void Awake()
    {
        base.Awake();
        entry = new HighscoreEntry(0, Systems.Instance.Nickname, Systems.Instance.difficultyLevel.DifficultyName);
        leaderboard = new Leaderboard();
        leaderboard.Load();
        highscore = leaderboard.GetHighscore(entry);
        highscoreText.gameObject.SetActive(false);
        DisplayScore();
    }

    private void Start()
    {
        multiplier = Systems.Instance.difficultyLevel.MultiplierIncrement;
    }


    public void AddPoints(float points)
    {
        if(entry.Score + points >= 0)
        {
            entry.Score += points;
        }     

        leaderboard.UpdateScore(entry);
        DisplayScore();
    }



    void DisplayScore()
    {
        if(highscore != 0 && entry.Score > highscore)
        {
            highscoreText.gameObject.SetActive(true);
        }
        else
        {
            highscoreText.gameObject.SetActive(false);
        }

        scoreDisplay.text = $"Score: {entry.Score} (X{multiplierCount + 1})";
    }

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


    public void ResetMultiplier()
    {
        multiplierCount = 0;
        DisplayScore();
    }

    public void DecrementMultiplier()
    {
        multiplierCount--;
        if(multiplierCount <= 0)
        {
            multiplierCount = 0;
        }
        DisplayScore();
    }

    public void IncrementMultiplier()
    {
        multiplierCount++;
    }
}
