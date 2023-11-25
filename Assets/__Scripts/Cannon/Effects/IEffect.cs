using UnityEngine;

public abstract class Effect : ScriptableObject
{
    public abstract void ApplyHitEffect(Projectile context);
}
