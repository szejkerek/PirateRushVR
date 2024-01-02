using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages environmental audio effects.
/// </summary>
public class EnviromentAudio : MonoBehaviour
{
    [SerializeField] private Sound waterSound;

    /// <summary>
    /// Plays the specified environmental audio on this GameObject when the script starts.
    /// </summary>
    private void Start()
    {
        AudioManager.Instance.PlayOnTarget(gameObject, waterSound);
    }
}
