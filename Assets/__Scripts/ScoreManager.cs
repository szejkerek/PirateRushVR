using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private void Awake()
    {
        Leaderboard l = new Leaderboard();
        l.UpdateScore("zjeb", 69);
        l.UpdateScore("zjeb", 20);
        l.UpdateScore("cwel", 120);
        l.UpdateScore("cwel", 240);
    }
}
