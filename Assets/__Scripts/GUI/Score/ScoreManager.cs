using System.Collections.Generic;
using System.Linq;
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
        entry = new HighscoreEntry(0, GlobalSettingManager.Instance.GetNickname(), Systems.Instance.difficultyLevel.DifficultyName);
        leaderboard = new Leaderboard();
        leaderboard.Load();

        CreateMockLeaderboardData();

        highscore = leaderboard.GetHighscore(entry);
        highscoreText.gameObject.SetActive(false);
        DisplayScore();
    }

    private void CreateMockLeaderboardData()
    {
        Random.State originalSeed = Random.state; 

        Random.InitState(123);

        List<string> allNicknames = new List<string> {
        "BeginnerBuddy", "EasyGoer", "NoSweat", "Chill Champ", "SimpleSimon",
        "RandomRider", "Moderate Master", "DecentDabbler", "AverageAce", "Midway Maven",
        "QueenOfChaos", "HardcoreHero", "ToughCookie", "Ruthless Ruler", "Challenge Champ",
        "Perceptive Pro", "StrategicSultan", "Limitless Legend", "Resilient Rider",
        "SupremeSovereign", "Whiz Wanderer", "TrailblazingTactician", "Sly Sorcerer",
        "UnstoppableUtopian", "Boundless Boss", "DaringDynamo", "MajesticMaestro",
        "UltimateUnderdog", "Ambitious Adventurer", "Intrepid Innovator", "Phenomenal Pathfinder"
        };

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
