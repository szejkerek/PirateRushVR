using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Shows the keyboard on selecting an input field and sets player preferences accordingly.
/// </summary>
public class ShowKeyboardOnSelect : MonoBehaviour
{
    TMP_InputField inputField;
    SetPlayerPreferences playerPreferences;

    private void Awake()
    {
#if UNITY_EDITOR
        Debug.LogWarning("Use system keyboard!");
#else
        playerPreferences = FindObjectOfType<SetPlayerPreferences>();
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(_ => playerPreferences.SetHandItems(HandHeldType.None));
        inputField.onDeselect.AddListener(_ => InvokeOnNextFrame());
#endif
    }

    /// <summary>
    /// Invokes a method on the next frame using a coroutine.
    /// </summary>
    private void InvokeOnNextFrame()
    {
        StartCoroutine(DelayedMethod());
    }

    /// <summary>
    /// Delays the execution of a method by one frame using a coroutine.
    /// </summary>
    /// <returns>An enumerator to control coroutine execution.</returns>
    private IEnumerator DelayedMethod()
    {
        yield return new WaitForSeconds(0.01f);
        playerPreferences.SetHandItems(HandHeldType.UIRays);
    }
}
