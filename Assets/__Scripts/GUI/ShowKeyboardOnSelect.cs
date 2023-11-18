using Microsoft.MixedReality.Toolkit.Experimental.UI;
using System;
using TMPro;
using UnityEngine;

public class ShowKeyboardOnSelect : MonoBehaviour
{
    [SerializeField] float smoothingFactor = 5.0f;
    [SerializeField] float verticalOffset = -0.5f;
    [SerializeField] float distance;
    [SerializeField] Transform positionSource;
    TMP_InputField inputField;

    private void Start()
    {
        inputField = GetComponent<TMP_InputField>();
        inputField.onSelect.AddListener(_ => ShowKeyboard());
        FollowTarget(lerp: false);
    }

    void ShowKeyboard()
    {
        NonNativeKeyboard.Instance.InputField = inputField;
        NonNativeKeyboard.Instance.PresentKeyboard(inputField.text);
        NonNativeKeyboard.Instance.OnClosed += ClearCaret;
    }

    void LateUpdate()
    {
        FollowTarget();
    }

    void FollowTarget(bool lerp = true)
    {
        Vector3 dir = positionSource.forward;
        dir.y = 0;
        dir.Normalize();
        Vector3 targetPosition = positionSource.position + dir * distance + Vector3.up * verticalOffset;

        Vector3 newPos;
        if (lerp)
        {
            newPos = Vector3.Lerp(NonNativeKeyboard.Instance.transform.position, targetPosition, Time.deltaTime * smoothingFactor);
        }
        else
        {
            newPos = targetPosition;
        }
        NonNativeKeyboard.Instance.RepositionKeyboard(newPos);
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
