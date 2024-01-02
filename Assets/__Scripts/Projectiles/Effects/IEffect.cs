using UnityEngine;

/// <summary>
/// Abstract class representing an effect applied by a projectile.
/// </summary>
public abstract class Effect : ScriptableObject
{
    /// <summary>
    /// Abstract method to apply the hit effect to the provided projectile context.
    /// </summary>
    /// <param name="context">The Projectile to which the effect is applied.</param>
    public abstract void ApplyHitEffect(Projectile context);
}
