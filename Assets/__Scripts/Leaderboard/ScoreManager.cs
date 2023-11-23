using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    private void Awake()
    {
        Leaderboard l = new Leaderboard();
        l.Load();
    }
}
