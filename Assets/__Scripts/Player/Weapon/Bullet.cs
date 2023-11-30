using DG.Tweening;
using UnityEngine;
public class Bullet : Weapon
{

    [SerializeField] float minExplosionForce;
    [SerializeField] float maxExplosionForce;
    [SerializeField] float explosionForceRadius;

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

        if(projectile.Data.FracturedModel != null)
        {
            GameObject obj = Instantiate(projectile.Data.FracturedModel, projectile.transform.position, projectile.transform.rotation);

            foreach (Transform t in obj.transform)
            {
                var rb = t.GetComponent<Rigidbody>();
                if(rb != null)
                {
                    rb.AddExplosionForce(Random.Range(minExplosionForce, maxExplosionForce), transform.position, explosionForceRadius);
                }

                Sequence sequence = DOTween.Sequence();
                sequence.AppendInterval(Random.Range(3, 5))
                    .AppendCallback(() => Destroy(t.gameObject)); 
                sequence.Play();
            }

        }

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
