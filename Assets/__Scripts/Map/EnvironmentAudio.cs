using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnviromentAudio : MonoBehaviour
{
    [SerializeField] private Sound waterSound;

    private void Start()
    {
        AudioManager.Instance.PlayOnTarget(gameObject, waterSound);
    }
}
