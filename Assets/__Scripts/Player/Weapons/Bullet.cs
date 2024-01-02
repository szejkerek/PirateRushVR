using DG.Tweening;
using UnityEngine;

/// <summary>
/// Represents a projectile fired as a bullet from a weapon.
/// </summary>
public class Bullet : Weapon
{
    [SerializeField] private Sound woodBreakSound;
    [SerializeField] private Sound splatSound;
    [Space]
    [SerializeField] float minExplosionForce;
    [SerializeField] float maxExplosionForce;
    [SerializeField] float explosionForceRadius;

    Projectile hitProjectile;
    Vector3 hitPoint;

    float perfectShootChance;

    private void Start()
    {
        perfectShootChance = Systems.Instance.difficultyLevel.PerfectShootChance;
        Destroy(gameObject, 6f);
    }

    bool hitOnce = false;

    /// <summary>
    /// Handles whether the bullet hit a projectile or not.
    /// </summary>
    protected override bool DidHit(out Projectile projectile, out Vector3 point, int projectileLayer)
    {
        point = Vector3.zero;
        if (!hitOnce && hitProjectile != null)
        {
            point = hitPoint;
            projectile = hitProjectile;
            Destroy(gameObject, 5f);
            hitOnce = true;
            return true;
        }

        projectile = null;
        return false;
    }

    /// <summary>
    /// Behavior of the bullet when interacting with a shootable object.
    /// </summary>
    protected override void ShootableBehavior(Projectile projectile, Vector3 point)
    {
        bool isPerfect = IsPerfect();
        projectile.ApplyEffects(critical: isPerfect);
        projectile.ApplyPoints(critical: isPerfect);

        if (projectile.Data.OptionalData.FracturedModel != null)
        {
            GameObject obj = Instantiate(projectile.Data.OptionalData.FracturedModel, projectile.transform.position, projectile.transform.rotation);

            foreach (Transform t in obj.transform)
            {
                MeshCollider collider = t.gameObject.AddComponent<MeshCollider>();
                collider.convex = true;

                t.gameObject.layer = LayerMask.NameToLayer("ProjectileNonCollision");
                Rigidbody rb = t.gameObject.AddComponent<Rigidbody>();

                if (rb != null)
                {
                    rb.mass = 0.01f;
                    rb.AddExplosionForce(Random.Range(minExplosionForce, maxExplosionForce), transform.position, explosionForceRadius);
                }
            }

            AudioManager.Instance.PlayAtPosition(point, woodBreakSound);

            Destroy(obj, Random.Range(3, 5));
        }

        Destroy(projectile.gameObject);
    }

    /// <summary>
    /// Behavior of the bullet when interacting with a sliceable object.
    /// </summary>
    protected override void SliceableBehavior(Projectile projectile, Vector3 point)
    {
        projectile.ApplyEffects(false);
        projectile.ApplyPoints(negative: true);
        AudioManager.Instance.PlayAtPosition(point, splatSound);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Projectile proj))
        {
            hitProjectile = proj;
            hitPoint = collision.GetContact(0).point;
        }
    }

    private bool IsPerfect()
    {
        return Random.Range(0f, 1f) <= perfectShootChance;
    }
}
