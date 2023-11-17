using Microsoft.MixedReality.Toolkit.Experimental.UI;
using System;
using TMPro;
using UnityEngine;

public class ShowKeyboardOnSelect : MonoBehaviour
{
    [SerializeField] float verticalOffset = -0.5f;
    [SerializeField] float distance;
    [SerializeField] Transform positionSource;
    TMP_InputField inputField;

    private void Awake()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(_ => ShowKeyboard());
    }

    void ShowKeyboard()
    {
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);

        NonNativeKeyboard.Instance.OnClosed += ClearCaret;
    }

    void LateUpdate()
    {
        SmoothlyFollowTarget();
    }

    void SmoothlyFollowTarget()
    {
        Vector3 dir = positionSource.forward;
        dir.y = 0;
        dir.Normalize();
        Vector3 targetPosition = positionSource.position + dir * distance + Vector3.up * verticalOffset;

        // Smoothly move the keyboard to the target position
        float smoothingFactor = 5.0f; // Adjust this value to control the smoothness of the movement
        Vector3 smoothedPosition = Vector3.Lerp(NonNativeKeyboard.Instance.transform.position, targetPosition, Time.deltaTime * smoothingFactor);
        NonNativeKeyboard.Instance.RepositionKeyboard(smoothedPosition);
    }

    public void HideKeyboard()
    {
        NonNativeKeyboard.Instance.Close();
        NonNativeKeyboard.Instance.OnClosed -= ClearCaret;
    }

    private void ClearCaret(object sender, EventArgs e)
    {
        NonNativeKeyboard.Instance.OnClosed -= ClearCaret;
    }
}
