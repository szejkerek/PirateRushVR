using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the functionality of the leaderboard panel, updating and displaying high score entries.
/// </summary>
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

    /// <summary>
    /// Hides the leaderboard panel and displays the back panel.
    /// </summary>
    void HideLeaderboard()
    {
        backPanel.SetActive(true);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Updates the leaderboard panel with high score entries for the selected difficulty level.
    /// </summary>
    void UpdateLeaderboard()
    {
        string difficultyName = Systems.Instance.difficultyLevel.DifficultyName;
        difficultyText.text = difficultyName;

        // Clear the previous entries in the leaderboard container
        foreach (Transform child in leaderboardContainer.transform)
        {
            Destroy(child.gameObject);
        }

        int lp = 1;
        Leaderboard leaderboard = ScoreManager.Instance.Leaderboard;

        // Display high score entries for the selected difficulty
        foreach (HighscoreEntry entry in leaderboard.HighscoreEntries)
        {
            if (entry.DifficultyName != difficultyName)
                continue;

            LeaderboardRow leaderboardRow = Instantiate(leaderboardEntry, leaderboardContainer);
            leaderboardRow.Init(lp, entry);
            lp++;
        }
    }
}
