using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetPlayerPreferences : MonoBehaviour
{
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

        left.Model.SetActive(true);
        right.Model.SetActive(true);
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
                left.UIRay.SetActive(true);
                right.UIRay.SetActive(true);
                break;
            case HandHeldType.TeleportRays:
                left.TeleportRay.SetActive(true);
                right.TeleportRay.SetActive(true);
                break;
            case HandHeldType.None:
                left.Model.SetActive(false);
                right.Model.SetActive(false);
                break;
        }
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
}
