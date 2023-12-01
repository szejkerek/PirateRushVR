using DG.Tweening;
using System;
using TMPro;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject startGamePanel;
    [SerializeField] GameObject endGamePanel;
    [SerializeField] TMP_Text nicknameDisplay;

    SetPlayerPreferences playerPrefs;
    void Start()
    {
        DisplayNickname();
        startGamePanel.SetActive(true);
        endGamePanel.SetActive(false);
        playerPrefs = FindObjectOfType<SetPlayerPreferences>();
        playerPrefs.SetHandItems(HandHeldType.UIRays);
        HealthManager.Instance.OnDeath += EndGame;
        CannonsManager.Instance.Pause();

        Sequence sequence = DOTween.Sequence();
        startGamePanel.transform.localScale = Vector3.zero;
        endGamePanel.transform.localScale = Vector3.zero;
        sequence.AppendInterval(1.0f);
        sequence.Append(startGamePanel.transform.DOScale(1f, 1.50f).SetEase(Ease.OutBounce));
    }

    public void DisplayNickname()
    {
        nicknameDisplay.text = Systems.Instance.Nickname;
    }

    public void StartGame()
    {
        SetHandItemsAccordingToPreference();
        startGamePanel.transform.DOScale(0f, 0.5f).OnComplete(() => {
            startGamePanel.SetActive(false);
        });

        CannonsManager.Instance.UnPause();
    }

    private void EndGame()
    {
        playerPrefs.SetHandItems(HandHeldType.UIRays);
        endGamePanel.SetActive(true);

        endGamePanel.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBounce);

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