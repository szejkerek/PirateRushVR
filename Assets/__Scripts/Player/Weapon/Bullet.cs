using UnityEngine;
public class Bullet : Weapon
{
    Projectile hitProjectile;
    Vector3 hitPoint;

    bool hitOnce = false;
    protected override bool DidHit(out Projectile projectile, out Vector3 point, int projectileLayer)
    {
        point = Vector3.zero;
        if (!hitOnce && hitProjectile != null)
        {
            point = hitPoint;
            projectile = hitProjectile;
            Destroy(gameObject, 3f);
            hitOnce = true;
            return true;
        }

        projectile = null;
        return false;
    }


    protected override void ShootableBehavior(Projectile projectile, Vector3 point)
    {
        bool isPerfect = IsPerfect();
        projectile.ApplyEffects(critical: isPerfect);
        projectile.ApplyPoints(critical: isPerfect);
        Destroy(projectile.gameObject);
    }

    protected override void SliceableBehavior(Projectile projectile, Vector3 point)
    {
        projectile.ApplyEffects(false);
        projectile.ApplyPoints(negative: true);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Projectile proj))
        {
            hitProjectile = proj;
            hitPoint = collision.GetContact(0).point;
        }
    }

    private bool IsPerfect()
    {
        return Random.Range(0, 5) == 0;
    }
}
