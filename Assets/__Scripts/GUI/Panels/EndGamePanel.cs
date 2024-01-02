using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the end game panel functionality.
/// </summary>
public class EndGamePanel : MonoBehaviour
{
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject leaderboardPanel;

    [Space]
    [SerializeField] Button restartBtn;
    [SerializeField] Button mainMenuBtn;
    [SerializeField] Button leaderboardBtn;

    private void Awake()
    {
        // Add listeners to buttons
        restartBtn.onClick.AddListener(RestartGame);
        mainMenuBtn.onClick.AddListener(LoadMainMenu);
        leaderboardBtn.onClick.AddListener(ShowLeaderboard);
    }

    /// <summary>
    /// Loads the main menu scene.
    /// </summary>
    void LoadMainMenu()
    {
        SceneLoader.Instance.LoadScene(SceneEnum.MainMenu);
    }

    /// <summary>
    /// Restarts the game by loading the gameplay scene.
    /// </summary>
    void RestartGame()
    {
        SceneLoader.Instance.LoadScene(SceneEnum.Gameplay);
    }

    /// <summary>
    /// Displays the leaderboard panel and hides the main panel.
    /// </summary>
    void ShowLeaderboard()
    {
        mainPanel.SetActive(false);
        leaderboardPanel.SetActive(true);
    }
}
