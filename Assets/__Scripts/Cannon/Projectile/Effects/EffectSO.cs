using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DebugEffect", menuName = "Cannon/Projectile/Effects/DebugEffect", order = 1)]
public class EffectSO : Effect
{
    [field: SerializeField] public string EffectName { private set; get; }

    public override void ApplyHitEffect(Projectile context)
    {
        Debug.Log(EffectName);
    }
}
