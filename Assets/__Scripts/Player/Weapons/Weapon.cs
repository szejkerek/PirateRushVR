using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] Sound collectibleSound;
    [SerializeField] LayerMask layerMask;
    
    private void Update()
    {
        if (DidHit(out Projectile projectile, out Vector3 point,layerMask))
        {
            Sound customSound = projectile.Data.OptionalData.CustomHitSound;
            if (customSound != null)
            {
                AudioManager.Instance.PlayAtPosition(point, customSound);    
            }

            switch (projectile.Data.ProjectileType)
            {
                case ProjectileType.Sliceable:
                    SliceableBehavior(projectile, point);
                    break;
                case ProjectileType.Shootable:
                    ShootableBehavior(projectile, point);
                    break;
                case ProjectileType.Collectible:
                    CollectibleBehavior(projectile, point);
                    break;
            }
        }
    }

    protected abstract bool DidHit(out Projectile hit, out Vector3 point, int projectileLayer);
    protected abstract void ShootableBehavior(Projectile projectile, Vector3 point);
    protected abstract void SliceableBehavior(Projectile projectile, Vector3 point);
    private void CollectibleBehavior(Projectile projectile, Vector3 point)
    {
        if (!projectile.TryGetComponent(out Bomb _))
        {
            AudioManager.Instance.PlayAtPosition(point, collectibleSound);
        }

        projectile.ApplyEffects(false);
        projectile.ApplyPoints();
        Destroy(projectile.gameObject);     
    }

}