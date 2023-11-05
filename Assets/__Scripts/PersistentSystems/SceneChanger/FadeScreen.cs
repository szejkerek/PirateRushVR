using System.Collections;
using UnityEngine;

public class FadeScreen : MonoBehaviour
{
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

    public void FadeIn()
    {
        Fade(1, 0);
    }

    public void FadeOut()
    {
        Fade(0, 1);
    }

    public void Fade(float alphaIn, float alphaOut)
    {
        StartCoroutine(FadeRoutine(alphaIn, alphaOut));
    }

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