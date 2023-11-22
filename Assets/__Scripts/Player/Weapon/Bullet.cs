using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Weapon
{
    [SerializeField] private float sphereRadius = 0.2f; // Adjust this radius as needed
    bool didHit = false;
    protected override bool DidHit(out Projectile projectile, int projectileLayer)
    {
        projectile = null;
        if (didHit)
            return false;

        Collider[] colliders = Physics.OverlapSphere(transform.position, sphereRadius, projectileLayer);

        if (colliders.Length > 0)
        {
            // Find the closest projectile among the colliders
            float closestDistance = Mathf.Infinity;
            Collider closestCollider = null;

            foreach (var collider in colliders)
            {
                float distance = Vector3.Distance(transform.position, collider.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCollider = collider;
                }
            }

            if (closestCollider != null)
            {
                if(closestCollider.TryGetComponent(out projectile))
                {
                    didHit = true;
                    return true;
                }
            }
        }  
        return false;
    }

    protected override void ShootableBehavior(Projectile projectile)
    {
        Debug.Log("Shootable");
        Destroy(projectile.gameObject);
    }

    protected override void SliceableBehavior(Projectile projectile)
    {
        Debug.Log("Sliceable");
        //throw new System.NotImplementedException();
        
    }
}
