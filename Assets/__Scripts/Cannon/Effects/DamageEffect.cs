using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DamageEffect", menuName = "Cannon/Projectile/MutualEffects/DamageEffect", order = 1)]
public class DamageEffect : Effect
{
    public override void ApplyHitEffect(Projectile context)
    {
        HealthManager.Instance.TakeHit();
    }
}
