using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetPlayerPreferences : MonoBehaviour
{
    [SerializeField] AnimateHandsOnInput leftAnim;
    [SerializeField] AnimateHandsOnInput rightAnim;

    [SerializeField] HandItems left;
    [SerializeField] HandItems right;

    ActionBasedSnapTurnProvider snapTurn;
    ActionBasedContinuousTurnProvider continuousTurn;

    void Start()
    {
        snapTurn = GetComponent<ActionBasedSnapTurnProvider>();
        continuousTurn = GetComponent<ActionBasedContinuousTurnProvider>();
        SetTurnType();
    }
    
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

    private void SetModelsActive(bool active)
    {
        left.Model.SetActive(active);
        right.Model.SetActive(active);
    }

    private void SetUIRaysActive(bool active)
    {
        left.UIRay.SetActive(active);
        right.UIRay.SetActive(active);
    }

    public void SetTurnType()
    {
        switch (GlobalSettingManager.Instance.GetTurnType())
        {
            case TurnType.Snap:
                EnableSnapTurn();
                break;
            case TurnType.Continuous:
                EnableContinuousTurn();
                break;
        }
    }

    private void EnableContinuousTurn()
    {
        snapTurn.leftHandSnapTurnAction.action.Disable();
        snapTurn.rightHandSnapTurnAction.action.Disable();
        continuousTurn.leftHandTurnAction.action.Enable();
        continuousTurn.rightHandTurnAction.action.Enable();
    }

    private void EnableSnapTurn()
    {
        snapTurn.leftHandSnapTurnAction.action.Enable();
        snapTurn.rightHandSnapTurnAction.action.Enable();
        continuousTurn.leftHandTurnAction.action.Disable();
        continuousTurn.rightHandTurnAction.action.Disable();
    }

    void SetAnimationsActive(bool active)
    {
        leftAnim.SetAnimationActive(active);
        rightAnim.SetAnimationActive(active);
    }
}
