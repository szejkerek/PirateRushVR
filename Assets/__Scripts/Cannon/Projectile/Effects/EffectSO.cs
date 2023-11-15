using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DebugEffect", menuName = "Cannon/Projectile/Effects/DebugEffect", order = 1)]
public class EffectSO : ScriptableObject, IEffect
{
    [field: SerializeField] public string EffectName { private set; get; }

    public void ApplySlicedEffect()
    {
        //Debug.Log(EffectName);
    }

    public void ApplyStartEffect()
    {
        //Debug.Log(EffectName);
    }
}
