using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DebugEffect", menuName = "Projectile/Effects/DebugEffect", order = 1)]
public class EffectSO : ScriptableObject, IEffect
{
    [field: SerializeField] public string EffectName { private set; get; }

    public void ApplyEffect()
    {
        //Debug.Log(EffectName);
    }
}
