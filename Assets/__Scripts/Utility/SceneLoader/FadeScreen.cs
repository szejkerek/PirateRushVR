using System.Collections;
using UnityEngine;

/// <summary>
/// Controls screen fading functionality.
/// </summary>
public class FadeScreen : MonoBehaviour
{
    /// <summary>
    /// Gets the duration of the fade effect.
    /// </summary>
    public float FadeDuration => fadeDuration;
    [SerializeField] float fadeDuration = 2f;

    [SerializeField] bool fadeOnStart;
    [SerializeField] Color fadeColor;
    Renderer rend;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        if (fadeOnStart)
        {
            FadeIn();
        }
    }

    /// <summary>
    /// Initiates the fade-in effect.
    /// </summary>
    public void FadeIn()
    {
        Fade(1, 0);
    }

    /// <summary>
    /// Initiates the fade-out effect.
    /// </summary>
    public void FadeOut()
    {
        FindObjectOfType<SetPlayerPreferences>()?.SetHandItems(HandHeldType.None);
        Fade(0, 1);
    }

    /// <summary>
    /// Initiates the fade effect with specified alpha values.
    /// </summary>
    /// <param name="alphaIn">Alpha value for fading in.</param>
    /// <param name="alphaOut">Alpha value for fading out.</param>
    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

    /// <summary>
    /// Coroutine that handles the fading effect over time.
    /// </summary>
    /// <param name="alphaIn">Alpha value for fading in.</param>
    /// <param name="alphaOut">Alpha value for fading out.</param>
    public IEnumerator FadeRoutine(float alphaIn, float alphaOut)
    {
        float timer = 0f;
        while (timer < fadeDuration)
        {
            Color newColor = fadeColor;
            newColor.a = Mathf.Lerp(alphaIn, alphaOut, timer / fadeDuration);
            rend.material.SetColor("_BaseColor", newColor);

            timer += Time.deltaTime;
            yield return null;
        }

        Color newColorOut = fadeColor;
        newColorOut.a = alphaOut;
        rend.material.SetColor("_BaseColor", newColorOut);
    }
}
