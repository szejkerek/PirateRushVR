using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FreezeEffect", menuName = "Cannon/Projectile/Effects/FreezeEffect", order = 1)]
public class FreezeEffect : Effect
{
    [field: SerializeField] public float Duration { private set; get; }
    public override void ApplyHitEffect(Projectile context)
    {
        FreezeManager.Instance.Freeze(Duration);
    }
}
