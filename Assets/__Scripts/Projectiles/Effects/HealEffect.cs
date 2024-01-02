using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject representing a healing effect applied by a projectile.
/// </summary>
[CreateAssetMenu(fileName = "HealEffect", menuName = "Cannon/Projectile/MutualEffects/HealEffect", order = 1)]
public class HealEffect : Effect
{
    /// <summary>
    /// Applies the hit effect to the provided projectile context, healing the health.
    /// </summary>
    /// <param name="context">The Projectile to which the effect is applied.</param>
    public override void ApplyHitEffect(Projectile context)
    {
        HealthManager.Instance.Heal();
    }
}
