using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Weapon
{
    [SerializeField] private float sphereRadius = 0.2f; // Adjust this radius as needed
    bool didHit = false;
    protected override bool DidHit(out Projectile hit, int projectileLayer)
    {
        hit = null;
        if (didHit)
            return false;

        Collider[] colliders = Physics.OverlapSphere(transform.position, sphereRadius, projectileLayer);

        if (colliders.Length > 0)
        {
            // Find the closest hit among the colliders
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
                hit = closestCollider.GetComponent<Projectile>();
                if(hit != null)
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
        //throw new System.NotImplementedException();
    }

    protected override void SliceableBehavior(Projectile projectile)
    {
        Debug.Log("Sliceable");
        //throw new System.NotImplementedException();
    }
}
