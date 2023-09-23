using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private void Update()
    {
        ActivateRay(leftTeleportationRay, leftActive, leftCancel);
        ActivateRay(rightTeleportationRay, rightActive, rightCancel);
    }

    void ActivateRay(GameObject teleportationRay, InputActionProperty active, InputActionProperty cancel)
    {
        bool shouldActivate = active.action.ReadValue<float>() > 0.1f && cancel.action.ReadValue<float>() == 0;
        teleportationRay.SetActive(shouldActivate);
    }
}
