using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartGamePanel : MonoBehaviour
{
    [SerializeField] Button endGameBtn;
    [SerializeField] Button tutorialBtn;
    [SerializeField] Button mainBtn;
    [Space]
    [SerializeField] GameObject mainPanel;
    [SerializeField] GameObject tutorialPanel;
    [Space]
    [SerializeField] Button nextTutorialPanel;
    [SerializeField] Button previousTutorialPanel;
    [SerializeField] TMP_Text tutorialTitle;
    [SerializeField] Image tutorialImage;
    [SerializeField] TMP_Text tutorialText;
    [SerializeField] List<TutorialPanelSO> tutorialPanels;

    int currentTutorialIndex = 0;

    private void Awake()
    {
        tutorialPanel.SetActive(false);
        endGameBtn.onClick.AddListener(BackToMenu);
        mainBtn.onClick.AddListener(HideTutorial);
        tutorialBtn.onClick.AddListener(ShowTutorial);
        nextTutorialPanel.onClick.AddListener(ShowNextTutorialPanel);
        previousTutorialPanel.onClick.AddListener(ShowPreviousTutorialPanel);
    }

    public void BackToMenu() 
    {
        SceneLoader.Instance.LoadScene(SceneEnum.MainMenu);
    }


    public void ShowTutorial()
    {
        if (tutorialPanels.Count <= 0)
            return;

        mainPanel.SetActive(false);
        tutorialPanel.SetActive(true);
        ShowTutorialPanel(0);
    }

    public void HideTutorial()
    {
        mainPanel.SetActive(true);
        tutorialPanel.SetActive(false);
    }

    private void ShowTutorialPanel(int index)
    {
        if (index >= 0 && index < tutorialPanels.Count)
        {
            TutorialPanelSO currentPanel = tutorialPanels[index];
            tutorialTitle.text = currentPanel.Title;
            tutorialImage.sprite = currentPanel.Image;
            tutorialText.text = currentPanel.Text;
        }
    }

    public void ShowNextTutorialPanel()
    {
        currentTutorialIndex++;
        if (currentTutorialIndex >= tutorialPanels.Count)
        {
            currentTutorialIndex = 0;
        }
        ShowTutorialPanel(currentTutorialIndex);
    }

    public void ShowPreviousTutorialPanel()
    {
        currentTutorialIndex--;
        if (currentTutorialIndex < 0)
        {
            currentTutorialIndex = tutorialPanels.Count - 1; // Go to the last panel
        }
        ShowTutorialPanel(currentTutorialIndex);
    }
}
