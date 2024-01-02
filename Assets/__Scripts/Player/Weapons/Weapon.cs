using UnityEngine;

/// <summary>
/// Represents an abstract class for different types of weapons.
/// </summary>
public abstract class Weapon : MonoBehaviour
{
    [SerializeField] LayerMask layerMask;
    [SerializeField] Sound collectibleSound;

    /// <summary>
    /// Checks for collisions and performs corresponding actions based on the projectile type.
    /// </summary>
    private void Update()
    {
        if (DidHit(out Projectile projectile, out Vector3 point, layerMask))
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

    /// <summary>
    /// Checks if the weapon hits something and retrieves information about the hit.
    /// </summary>
    /// <param name="hit">The hit projectile.</param>
    /// <param name="point">The point of impact.</param>
    /// <param name="projectileLayer">The layer of the projectile.</param>
    /// <returns>True if a hit is detected; otherwise, false.</returns>
    protected abstract bool DidHit(out Projectile hit, out Vector3 point, int projectileLayer);

    /// <summary>
    /// Defines behavior for shootable projectiles upon hit.
    /// </summary>
    /// <param name="projectile">The shootable projectile hit.</param>
    /// <param name="point">The point of impact.</param>
    protected abstract void ShootableBehavior(Projectile projectile, Vector3 point);

    /// <summary>
    /// Defines behavior for sliceable projectiles upon hit.
    /// </summary>
    /// <param name="projectile">The sliceable projectile hit.</param>
    /// <param name="point">The point of impact.</param>
    protected abstract void SliceableBehavior(Projectile projectile, Vector3 point);

    /// <summary>
    /// Defines behavior for collectible projectiles upon hit.
    /// </summary>
    /// <param name="projectile">The collectible projectile hit.</param>
    /// <param name="point">The point of impact.</param>
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
