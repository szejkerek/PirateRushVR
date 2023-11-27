using System;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject startGamePanel;
    [SerializeField] GameObject endGamePanel;

    SetPlayerPreferences playerPrefs;
    void Start()
    {
        startGamePanel.SetActive(true);
        endGamePanel.SetActive(false);
        playerPrefs = FindObjectOfType<SetPlayerPreferences>();
        playerPrefs.SetHandItems(HandHeldType.UIRays);
        HealthManager.Instance.OnDeath += EndGame;
        CannonsManager.Instance.Pause();
    }

    public void StartGame()
    {
        SetHandItemsAccordingToPreference();
        startGamePanel.SetActive(false);
        CannonsManager.Instance.UnPause();
    }    

    private void EndGame()
    {
        playerPrefs.SetHandItems(HandHeldType.UIRays);
        endGamePanel.SetActive(true);
        CannonsManager.Instance.Pause();
        CannonsManager.Instance.ClearAllProjectiles();   
    }


    void SetHandItemsAccordingToPreference()
    {
        HandHeldType handPreference;
        if (Systems.Instance.KatanaRight)
        {
            handPreference = HandHeldType.PistolLeftKatanaRight;
        }
        else
        {
            handPreference = HandHeldType.KatanaLeftPistolRight;
        }

        playerPrefs.SetHandItems(handPreference);
    }
}