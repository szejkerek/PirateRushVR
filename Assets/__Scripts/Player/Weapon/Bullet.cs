using UnityEngine;

public class Bullet : Weapon
{
    Projectile hitProjectile;
    bool hitOnce = false;
    protected override bool DidHit(out Projectile projectile, int projectileLayer)
    {
        if (!hitOnce && hitProjectile != null)
        {
            hitOnce = true;
            projectile = hitProjectile;
            Destroy(gameObject, 3f);
            return true;
        }

        projectile = null;
        return false;
    }


    protected override void ShootableBehavior(Projectile projectile)
    {
        Debug.Log("Shootable");
        projectile.ApplyEffects(false);
        projectile.ApplyPoints();
        Destroy(projectile.gameObject);
    }

    protected override void SliceableBehavior(Projectile projectile)
    {
        Debug.Log("Sliceable");
        projectile.ApplyEffects(false);
        projectile.ApplyPoints(negative: true);

    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.TryGetComponent(out Projectile proj))
        {
            hitProjectile = proj;
        }
    }
}
