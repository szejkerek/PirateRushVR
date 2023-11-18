using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class UIWarning : MonoBehaviour
{
    TMP_Text warningText;
    Coroutine coroutine;

    private void Awake()
    {
        warningText= GetComponent<TMP_Text>();
        warningText.text = "";
    }

    public void ShowWarning(string text)
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        coroutine = StartCoroutine(ShowAndFadeWarning(warningText, text));
    }

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
