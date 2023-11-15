using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class SetPlayerPreferences : MonoBehaviour
{
    ActionBasedSnapTurnProvider snapTurn;
    ActionBasedContinuousTurnProvider continuousTurn;

    void Awake()
    {
        snapTurn = GetComponent<ActionBasedSnapTurnProvider>();
        continuousTurn = GetComponent<ActionBasedContinuousTurnProvider>();
        ApplyPlayerPref();
    }

    public void ApplyPlayerPref()
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
