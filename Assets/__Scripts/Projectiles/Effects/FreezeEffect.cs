using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject representing a freeze effect applied by a projectile.
/// </summary>
[CreateAssetMenu(fileName = "FreezeEffect", menuName = "Cannon/Projectile/MutualEffects/FreezeEffect", order = 1)]
public class FreezeEffect : Effect
{
    /// <summary>
    /// Duration of the freeze effect.
    /// </summary>
    [field: SerializeField] public float Duration { private set; get; }

    /// <summary>
    /// Applies the hit effect to the provided projectile context, freezing time for the specified duration.
    /// </summary>
    /// <param name="context">The Projectile to which the effect is applied.</param>
    public override void ApplyHitEffect(Projectile context)
    {
        SlowMotionManager.Instance.Freeze(Duration);
    }
}
