using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject representing a damage effect applied by a projectile.
/// </summary>
[CreateAssetMenu(fileName = "DamageEffect", menuName = "Cannon/Projectile/MutualEffects/DamageEffect", order = 1)]
public class DamageEffect : Effect
{
    /// <summary>
    /// Applies the hit effect to the provided projectile context.
    /// </summary>
    /// <param name="context">The Projectile to which the effect is applied.</param>
    public override void ApplyHitEffect(Projectile context)
    {
        HealthManager.Instance.TakeHit();
    }
}
