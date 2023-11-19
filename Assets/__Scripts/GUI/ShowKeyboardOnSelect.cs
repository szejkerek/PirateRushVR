using System.Collections;
using TMPro;
using UnityEngine;

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

    private void InvokeOnNextFrame()
    {
        StartCoroutine(DelayedMethod());
    }

    private IEnumerator DelayedMethod()
    {
        yield return new WaitForSeconds(0.01f);
        playerPreferences.SetHandItems(HandHeldType.UIRays);
    }

}
