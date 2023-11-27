using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    private void Update()
    {
        if (DidHit(out Projectile projectile, layerMask))
        {
            switch (projectile.Data.ProjectileType)
            {
                case ProjectileType.Sliceable:
                    SliceableBehavior(projectile);
                    break;
                case ProjectileType.Shootable:
                    ShootableBehavior(projectile);
                    break;
                case ProjectileType.Collectible:
                    CollectibleBehavior(projectile);
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
        projectile.ApplyPoints();
        Destroy(projectile.gameObject);     
        Debug.Log("Hit collectible layer");
    }

}