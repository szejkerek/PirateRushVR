using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Manages player preferences for VR interactions, hand items, vignette usage, and turn type.
/// </summary>
public class SetPlayerPreferences : MonoBehaviour
{
    [SerializeField] AnimateHandsOnInput leftAnim;
    [SerializeField] AnimateHandsOnInput rightAnim;

    [SerializeField] HandItems left;
    [SerializeField] HandItems right;
    [Space]
    [SerializeField] GameObject vignette;

    ActionBasedSnapTurnProvider snapTurn;
    ActionBasedContinuousTurnProvider continuousTurn;

    void Start()
    {
        snapTurn = GetComponent<ActionBasedSnapTurnProvider>();
        continuousTurn = GetComponent<ActionBasedContinuousTurnProvider>();
        SetTurnType(GlobalSettingManager.Instance.GetTurnType());
        SetVignetteUsage(GlobalSettingManager.Instance.GetVignetteUsage());
    }

    /// <summary>
    /// Sets vignette usage based on the given argument.
    /// </summary>
    /// <param name="arg">Boolean indicating whether to activate vignette.</param>
    public void SetVignetteUsage(bool arg)
    {
        vignette.SetActive(arg);
    }

    /// <summary>
    /// Sets hand items based on the provided hand-held type.
    /// </summary>
    /// <param name="type">The type of hand-held items to set.</param>
    public void SetHandItems(HandHeldType type)
    {
        left.TurnOffAll();
        right.TurnOffAll();     
        SetAnimationsActive(false);

        switch (type)
        {
            case HandHeldType.PistolLeftKatanaRight:
                left.Pistol.SetActive(true);
                left.PistolHand.SetActive(true);
                right.Katana.SetActive(true);
                right.KatanaHand.SetActive(true);
                break;
            case HandHeldType.KatanaLeftPistolRight:
                left.Katana.SetActive(true);
                left.KatanaHand.SetActive(true);
                right.Pistol.SetActive(true);
                right.PistolHand.SetActive(true);
                break;
            case HandHeldType.UIRays:
                SetAnimationsActive(true);
                SetModelsActive(true);
                SetUIRaysActive(true);
                break;
            case HandHeldType.FreeHands:
                SetModelsActive(true);
                break;
            case HandHeldType.None:
                left.TurnOffAll();
                right.TurnOffAll();
                break;
        }
    }

    /// <summary>
    /// Sets the visibility of hand models.
    /// </summary>
    /// <param name="active">Boolean indicating the visibility status of hand models.</param>

    private void SetModelsActive(bool active)
    {
        left.Model.SetActive(active);
        right.Model.SetActive(active);
    }

    /// <summary>
    /// Sets the visibility of UI rays.
    /// </summary>
    /// <param name="active">Boolean indicating the visibility status of UI rays.</param>
    private void SetUIRaysActive(bool active)
    {
        left.UIRay.SetActive(active);
        right.UIRay.SetActive(active);
    }

    /// <summary>
    /// Sets the turn type based on the provided type.
    /// </summary>
    /// <param name="type">The type of turn to be set.</param>
    public void SetTurnType(TurnType type)
    {
        switch (type)
        {
            case TurnType.Snap:
                EnableSnapTurn();
                break;
            case TurnType.Continuous:
                EnableContinuousTurn();
                break;
        }
    }

    /// <summary>
    /// Enables continuous turn action and disables snap turn action.
    /// </summary>
    private void EnableContinuousTurn()
    {
        snapTurn.leftHandSnapTurnAction.action.Disable();
        snapTurn.rightHandSnapTurnAction.action.Disable();
        continuousTurn.leftHandTurnAction.action.Enable();
        continuousTurn.rightHandTurnAction.action.Enable();
    }

    /// <summary>
    /// Enables snap turn action and disables continuous turn action.
    /// </summary>
    private void EnableSnapTurn()
    {
        snapTurn.leftHandSnapTurnAction.action.Enable();
        snapTurn.rightHandSnapTurnAction.action.Enable();
        continuousTurn.leftHandTurnAction.action.Disable();
        continuousTurn.rightHandTurnAction.action.Disable();
    }

    /// <summary>
    /// Sets the state of animations for hands.
    /// </summary>
    /// <param name="active">Boolean indicating the state of hand animations.</param>
    void SetAnimationsActive(bool active)
    {
        leftAnim.SetAnimationActive(active);
        rightAnim.SetAnimationActive(active);
    }
}
