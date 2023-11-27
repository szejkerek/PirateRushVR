using System.Collections;
using UnityEngine;

public class FreezeManager : Singleton<FreezeManager>
{
    [SerializeField] float freezeDuration;
    [SerializeField] float unfreezeDuration;

    [SerializeField] float targetFreeze;

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

        while (currentTime < freezeDuration)
        {
            Time.timeScale = Mathf.Lerp(initialTimeScale, targetFreeze, currentTime / freezeDuration);
            currentTime += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = targetFreeze;

        currentTime = 0f;
        while (currentTime < duration)
        {
            currentTime += Time.unscaledDeltaTime;
            yield return null;
        }

        currentTime = 0f;
        
        initialTimeScale = targetFreeze;
        while (currentTime < unfreezeDuration)
        {
            Time.timeScale = Mathf.Lerp(initialTimeScale, 1.0f, currentTime / unfreezeDuration);
            currentTime += Time.unscaledDeltaTime;
            yield return null;
        }
        Time.timeScale = 1.0f;
    }
}
