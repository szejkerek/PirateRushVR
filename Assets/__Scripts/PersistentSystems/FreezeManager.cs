using System.Collections;
using UnityEngine;

public class FreezeManager : Singleton<FreezeManager>
{
    [SerializeField] float freezingSpeed;
    [SerializeField] float unfreezingSpeed;

    [SerializeField] float targetFreezeValue;

    Coroutine freezeCoroutine;

    public void Freeze(float duration)
    {
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
        }

        freezeCoroutine = StartCoroutine(ChangeTimeScaleOverDuration(duration));
    }


    IEnumerator ChangeTimeScaleOverDuration(float duration)
    {
        float currentTime = 0f;
        float initialTimeScale = Time.timeScale;

        while (currentTime < freezingSpeed)
        {
            Time.timeScale = Mathf.Lerp(initialTimeScale, targetFreezeValue, currentTime / freezingSpeed);
            currentTime += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = targetFreezeValue;

        currentTime = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.unscaledDeltaTime;
            yield return null;
        }

        currentTime = 0f;
        
        initialTimeScale = targetFreezeValue;
        while (currentTime < unfreezingSpeed)
        {
            Time.timeScale = Mathf.Lerp(initialTimeScale, 1.0f, currentTime / unfreezingSpeed);
            currentTime += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1.0f;
    }
}
