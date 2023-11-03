using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEffect : MonoBehaviour, IEffect
{
    public void ApplyEffect()
    {
        Debug.Log("Debug Effect");
    }
}
