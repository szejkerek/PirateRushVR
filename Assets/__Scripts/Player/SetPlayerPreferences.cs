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

    void Awake()
    {
        snapTurn = GetComponent<ActionBasedSnapTurnProvider>();
        continuousTurn = GetComponent<ActionBasedContinuousTurnProvider>();
        SetTurnType();
    }
    
    public void SetHandItems(HandHeldType type)
    {
        left.TurnOffAll();
        right.TurnOffAll();

        SetModelsActive(true);
        SetAnimationsActive(false);

        switch (type)
        {
            case HandHeldType.PistolLeftKatanaRight:
                left.Pistol.SetActive(true);
                right.Katana.SetActive(true);
                break;
            case HandHeldType.KatanaLeftPistolRight:
                left.Katana.SetActive(true);
                right.Pistol.SetActive(true);
                break;
            case HandHeldType.UIRays:
                SetAnimationsActive(true);
                SetUIRaysActive(true);
                break;
            case HandHeldType.None:
                SetModelsActive(false);
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
        if (!PlayerPrefs.HasKey("turn"))
            return;

        bool useContinuousTurn = Convert.ToBoolean(PlayerPrefs.GetInt("turn"));
        if(!useContinuousTurn)
        {
            snapTurn.leftHandSnapTurnAction.action.Enable();
            snapTurn.rightHandSnapTurnAction.action.Enable();
            continuousTurn.leftHandTurnAction.action.Disable();
            continuousTurn.rightHandTurnAction.action.Disable();
        }
        else
        {
            snapTurn.leftHandSnapTurnAction.action.Disable();
            snapTurn.rightHandSnapTurnAction.action.Disable();
            continuousTurn.leftHandTurnAction.action.Enable();
            continuousTurn.rightHandTurnAction.action.Enable();
        }
    }

    void SetAnimationsActive(bool active)
    {
        leftAnim.SetAnimationActive(active);
        rightAnim.SetAnimationActive(active);
    }
}
