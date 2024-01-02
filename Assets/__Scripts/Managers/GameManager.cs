using DG.Tweening;
using UnityEngine;

/// <summary>
/// Manages the game flow and UI transitions.
/// </summary>
public class GameManager : Singleton<GameManager>
{
    [SerializeField] GameObject startGamePanel;
    [SerializeField] GameObject endGamePanel;

    SetPlayerPreferences playerPrefs;

    void Start()
    {
        startGamePanel.SetActive(true);
        endGamePanel.SetActive(false);

        // Setting up player preferences and event subscriptions
        playerPrefs = FindObjectOfType<SetPlayerPreferences>();
        playerPrefs.SetHandItems(HandHeldType.UIRays);
        HealthManager.Instance.OnDeath += EndGame;
        CannonsManager.Instance.Pause();

        // Tweening sequence for UI scaling animation
        Sequence sequence = DOTween.Sequence();
        startGamePanel.transform.localScale = Vector3.zero;
        endGamePanel.transform.localScale = Vector3.zero;
        sequence.AppendInterval(1.0f);
        sequence.Append(startGamePanel.transform.DOScale(1f, 1.50f).SetEase(Ease.OutBounce));
    }

    /// <summary>
    /// Initiates the game.
    /// </summary>
    public void StartGame()
    {
        SetHandItemsAccordingToPreference();

        // Hides the start game panel and resumes game
        startGamePanel.transform.DOScale(0f, 0.5f).OnComplete(() => {
            startGamePanel.SetActive(false);
        });

        CannonsManager.Instance.UnPause();
    }

    /// <summary>
    /// Handles actions at the end of the game.
    /// </summary>
    private void EndGame()
    {
        // Resets hand items and displays end game panel
        playerPrefs.SetHandItems(HandHeldType.UIRays);
        endGamePanel.SetActive(true);
        endGamePanel.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBounce);

        // Pauses cannons and clears projectiles
        CannonsManager.Instance.Pause();
        CannonsManager.Instance.ClearAllProjectiles();
    }

    /// <summary>
    /// Sets hand items according to player preference.
    /// </summary>
    void SetHandItemsAccordingToPreference()
    {
        HandHeldType handPreference;

        // Determines hand preference based on game settings
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
