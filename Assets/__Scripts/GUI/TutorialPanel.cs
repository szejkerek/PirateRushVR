using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPanel : MonoBehaviour
{
    [SerializeField] GameObject backPanel;
    [SerializeField] Button backBtn;
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
        backBtn.onClick.AddListener(HidePanel);
        nextTutorialPanel.onClick.AddListener(ShowNextTutorialPanel);
        previousTutorialPanel.onClick.AddListener(ShowPreviousTutorialPanel);
    }

    private void OnEnable()
    {
        if (tutorialPanels.Count <= 0)
            return;
        ShowTutorialPanel(0);
    }

    private void HidePanel()
    {
        backPanel.SetActive(true);
        gameObject.SetActive(false);
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
