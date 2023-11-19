using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    private void Update()
    {
        if (DidHit(out Projectile projectile, layerMask))
        {
            switch (projectile.Data.Type)
            {
                case ComboSpawnType.NeutralProjectile:
                    SliceableBehavior(projectile);
                    break;
                case ComboSpawnType.Bomb:
                    CollectibleBehavior(projectile);
                    break;
                case ComboSpawnType.SpecialItem:
                    ShootableBehavior(projectile);
                    break;
            }     
        }
    }

    protected abstract bool DidHit(out Projectile hit, int projectileLayer);
    protected abstract void ShootableBehavior(Projectile projectile);
    protected abstract void SliceableBehavior(Projectile projectile);
    private void CollectibleBehavior(Projectile projectile)
    {
        projectile.ApplyEffects(false);
        Debug.Log("Hit collectible layer");
        Destroy(projectile.gameObject);     
    }
    
}