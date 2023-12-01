using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartGamePanel : MonoBehaviour
{
    [SerializeField] Button endGameBtn;
    [SerializeField] Button tutorialBtn;
    [SerializeField] Button leaderboardBtn;
    [Space]
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject leaderboardPanel;
    [SerializeField] GameObject tutorialPanel;


    private void Awake()
    {
        tutorialPanel.SetActive(false);
        endGameBtn.onClick.AddListener(BackToMenu);
        leaderboardBtn.onClick.AddListener(ShowLeaderboard);
        tutorialBtn.onClick.AddListener(ShowTutorial);

    }

    public void BackToMenu() 
    {
        SceneLoader.Instance.LoadScene(SceneEnum.MainMenu);
    }


    public void ShowTutorial()
    {
        mainPanel.SetActive(false);
        tutorialPanel.SetActive(true);
    }       
    
    public void ShowLeaderboard()
    {
        mainPanel.SetActive(false);
        leaderboardPanel.SetActive(true);
    }   
}
