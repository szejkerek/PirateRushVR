using System.Collections;
using TMPro;
using UnityEngine;

/// <summary>
/// Represents a UI element for displaying warnings with fading effect.
/// </summary>
[RequireComponent(typeof(TMP_Text))]
public class UIWarning : MonoBehaviour
{
    private TMP_Text warningText;
    private Coroutine coroutine;

    /// <summary>
    /// Initializes the warningText variable.
    /// </summary>
    private void Awake()
    {
        warningText = GetComponent<TMP_Text>();
        warningText.text = "";
    }

    /// <summary>
    /// Shows a warning message on the UI.
    /// </summary>
    /// <param name="text">The warning message to display.</param>
    public void ShowWarning(string text)
    {
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(ShowAndFadeWarning(warningText, text));
    }

    /// <summary>
    /// Displays the warning message and fades it out over time.
    /// </summary>
    /// <param name="warning">The TextMeshPro text component for displaying the warning.</param>
    /// <param name="text">The warning message to display.</param>
    /// <returns>An IEnumerator for the coroutine process.</returns>
    private IEnumerator ShowAndFadeWarning(TMP_Text warning, string text)
    {
        warning.text = text;
        Color originalColor = warning.color;

        warning.enabled = true;
        warning.color = new Color(originalColor.r, originalColor.g, originalColor.b, 1);

        yield return new WaitForSeconds(3f);

        float fadeDuration = 1f;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            warning.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
            yield return null;
        }

        warning.enabled = false;
        warning.text = "";
        warning.color = originalColor;
    }
}
