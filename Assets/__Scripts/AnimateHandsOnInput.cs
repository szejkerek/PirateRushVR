using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AnimateHandsOnInput : MonoBehaviour
{
    [SerializeField] private InputActionProperty pichAnimationAction;
    [SerializeField] private InputActionProperty gripAnimationAction;

    private Animator handAnimator;

    void Awake()
    {
        handAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        float triggerValue = pichAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}
