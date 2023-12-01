using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
public class AnimateHandsOnInput : MonoBehaviour
{
    [SerializeField] private InputActionProperty pichAnimationAction;
    [SerializeField] private InputActionProperty gripAnimationAction;

    private Animator handAnimator;
    bool shouldAnimate = true;
    void Awake()
    {
        handAnimator = GetComponent<Animator>();
    }

    public void SetAnimationActive(bool active)
    {
        shouldAnimate = active;
    }

    void Update()
    {
        if (!shouldAnimate)
            return;

        float triggerValue = pichAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Trigger", triggerValue);

        float gripValue = gripAnimationAction.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}
