using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Animates hands based on input actions like pinch and grip.
/// </summary>
[RequireComponent(typeof(Animator))]
public class AnimateHandsOnInput : MonoBehaviour
{
    [SerializeField] private InputActionProperty pichAnimationAction;
    [SerializeField] private InputActionProperty gripAnimationAction;

    private Animator handAnimator;
    bool shouldAnimate = true;

    /// <summary>
    /// Initialize the hand animator component.
    /// </summary>
    void Awake()
    {
        handAnimator = GetComponent<Animator>();
    }

    /// <summary>
    /// Sets whether hand animations should be active.
    /// </summary>
    /// <param name="active">Flag indicating if animations should be active.</param>
    public void SetAnimationActive(bool active)
    {
        shouldAnimate = active;
    }

    /// <summary>
    /// Updates hand animations based on input actions if animations are allowed.
    /// </summary>
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
