using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HealEffect", menuName = "Cannon/Projectile/Effects/HealEffect", order = 1)]
public class HealEffect : Effect
{
    public override void ApplyHitEffect(Projectile context)
    {
        HealthManager.Instance.Heal();
    }
}
