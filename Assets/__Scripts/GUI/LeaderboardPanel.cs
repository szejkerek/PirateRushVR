using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.EventSystems.EventTrigger;

public class LeaderboardPanel : MonoBehaviour
{
    [SerializeField] GameObject backPanel;
    [SerializeField] Button backBtn;
    [Space]
    [SerializeField] TMP_Text difficultyText;
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
        string difficultyName = Systems.Instance.difficultyLevel.DifficultyName;
        difficultyText.text = difficultyName;

        foreach (Transform child in leaderboardContainer.transform)
        {
            Destroy(child.gameObject);
        }
        
        Leaderboard leaderboard = ScoreManager.Instance.Leaderboard;
        for (int i = 0; i < leaderboard.HighscoreEntries.Count; i++)
        {
            HighscoreEntry entry = leaderboard.HighscoreEntries[i];
            if (entry.DifficultyName != difficultyName)
                continue;

            LeaderboardRow leaderboardRow = Instantiate(leaderboardEntry, leaderboardContainer);
            leaderboardRow.Init(i+1, entry);
        }

    }
}
