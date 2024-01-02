using System.Collections;
using UnityEngine;

/// <summary>
/// Manages the slow-motion effects in the game.
/// </summary>
public class SlowMotionManager : Singleton<SlowMotionManager>
{
    [SerializeField] float freezingSpeed;
    [SerializeField] float unfreezingSpeed;

    [SerializeField] float targetFreezeValue;

    Coroutine freezeCoroutine;

    /// <summary>
    /// Freezes time for a specified duration.
    /// </summary>
    /// <param name="duration">Duration for which time will be frozen.</param>
    public void Freeze(float duration)
    {
        if (freezeCoroutine != null)
        {
            StopCoroutine(freezeCoroutine);
        }

        freezeCoroutine = StartCoroutine(ChangeTimeScaleOverDuration(duration));
    }

    /// <summary>
    /// Changes the time scale gradually over a specified duration to simulate freezing and unfreezing time.
    /// </summary>
    /// <param name="duration">Duration over which the time scale will change.</param>
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
