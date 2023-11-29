using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardPanel : MonoBehaviour
{
    [SerializeField] GameObject backPanel;
    [SerializeField] Button backBtn;
    [Space]
    [SerializeField] Transform leaderboardContainer;
    [SerializeField] LeaderboardRow leaderboardEntry;

    private void Awake()
    {
        backBtn.onClick.AddListener(HideLeaderboard);
    }

    private void OnEnable()
    {
        UpdateLeaderboard();
    }

    void HideLeaderboard()
    {
        backPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    void UpdateLeaderboard()
    {
        foreach (Transform child in leaderboardContainer.transform)
        {
            Destroy(child.gameObject);
        }
        string difficultyName = Systems.Instance.difficultyLevel.DifficultyName;
        Leaderboard leaderboard = ScoreManager.Instance.Leaderboard;

        foreach (HighscoreEntry entry in leaderboard.HighscoreEntries)
        {
            if (entry.DifficultyName != difficultyName)
                continue;

            LeaderboardRow leaderboardRow = Instantiate(leaderboardEntry, leaderboardContainer);
            leaderboardRow.Init(entry);
        }
    }
}
