using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

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
        restartBtn.onClick.AddListener(RestartGame);
        mainMenuBtn.onClick.AddListener(LoadMainMenu);
        leaderboardBtn.onClick.AddListener(ShowLeaderboard);
    }

    void LoadMainMenu()
    {
        SceneLoader.Instance.LoadScene(SceneEnum.MainMenu);
    }

    void RestartGame()
    {
        SceneLoader.Instance.LoadScene(SceneEnum.Gameplay);
    }

    void ShowLeaderboard()
    {
        mainPanel.SetActive(false);
        leaderboardPanel.SetActive(true);
    }
}
