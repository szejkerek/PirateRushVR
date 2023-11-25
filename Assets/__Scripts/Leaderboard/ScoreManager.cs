  using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    Leaderboard leaderboard;

    protected override void Awake()
    {
        base.Awake();
        leaderboard = new Leaderboard();
        leaderboard.Load();
    }

    public void AddPoints(float points)
    {
        string currentNickname = Systems.Instance.Nickname;
        leaderboard.UpdateScore(currentNickname, points);


    }
}
