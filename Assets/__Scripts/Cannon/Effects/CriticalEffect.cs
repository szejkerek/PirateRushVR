using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CriticalEffect", menuName = "Cannon/Projectile/MutualEffects/CriticalEffect", order = 1)]
public class CriticalEffect : Effect
{
    public override void ApplyHitEffect(Projectile context)
    {
        switch (context.Data.ProjectileType)
        {
            case ProjectileType.Sliceable:
                AudioManager.Instance.PlayGlobal(AudioManager.Instance.SFXLib.PerfectSlice);
                Debug.Log("Perfect SliceableBehavior");
                break;
            case ProjectileType.Shootable:
                break;
            case ProjectileType.Collectible:
                break;
        }
    }
}
