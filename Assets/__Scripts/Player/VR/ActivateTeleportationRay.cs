using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Controls the activation and deactivation of teleportation rays based on input actions.
/// </summary>
public class ActivateTeleportationRay : MonoBehaviour
{
    [SerializeField] private GameObject leftTeleportationRay;
    [SerializeField] private GameObject rightTeleportationRay;

    [Header("Active")]
    [SerializeField] private InputActionProperty leftActive;
    [SerializeField] private InputActionProperty rightActive;

    [Header("Cancel")]
    [SerializeField] private InputActionProperty leftCancel;
    [SerializeField] private InputActionProperty rightCancel;

    /// <summary>
    /// Checks for input actions and activates or deactivates the teleportation rays accordingly.
    /// </summary>
    private void Update()
    {
        ActivateRay(leftTeleportationRay, leftActive, leftCancel);
        ActivateRay(rightTeleportationRay, rightActive, rightCancel);
    }

    /// <summary>
    /// Activates or deactivates the specified teleportation ray based on input actions.
    /// </summary>
    /// <param name="teleportationRay">The teleportation ray GameObject to be activated or deactivated.</param>
    /// <param name="active">The input action property for activation.</param>
    /// <param name="cancel">The input action property for cancellation.</param>
    void ActivateRay(GameObject teleportationRay, InputActionProperty active, InputActionProperty cancel)
    {
        bool shouldActivate = active.action.ReadValue<float>() > 0.1f && cancel.action.ReadValue<float>() == 0;
        teleportationRay.SetActive(shouldActivate);
    }
}
