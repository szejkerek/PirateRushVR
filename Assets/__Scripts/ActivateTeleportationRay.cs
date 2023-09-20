using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ActivateTeleportationRay : MonoBehaviour
{
    [SerializeField] private GameObject leftTeleportationRay;
    [SerializeField] private GameObject rightTeleportationRay;

    [SerializeField] private InputActionProperty leftActive;
    [SerializeField] private InputActionProperty rightActive;

    private void Update()
    {
        ActivateRay(leftTeleportationRay, leftActive);
        ActivateRay(rightTeleportationRay, rightActive);
    }

    void ActivateRay(GameObject teleportationRay, InputActionProperty condition)
    {
        bool shouldActivate = condition.action.ReadValue<float>() > 0.1f;
        teleportationRay.SetActive(shouldActivate);
    }
}
