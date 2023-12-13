using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FreezeEffect", menuName = "Cannon/Projectile/MutualEffects/FreezeEffect", order = 1)]
public class FreezeEffect : Effect
{
    [field: SerializeField] public float Duration { private set; get; }
    public override void ApplyHitEffect(Projectile context)
    {
        SlowMotionManager.Instance.Freeze(Duration);
    }
}
