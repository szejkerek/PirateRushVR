using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObjectParabolic : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float height;
    [SerializeField] private float gravity;
    [SerializeField] private Transform shootingPoint;

    public void LunchProjectile(Transform target, Projectile projectile)
    {
        Vector3 direction = CalculateDirection(target);

        Projectile obj = Instantiate(projectile, shootingPoint);

        obj.SetGravity(gravity);
        obj.SetVelocity(direction * speed);
    }

    private Vector3 CalculateDirection(Transform target)
    {
        return target.position - transform.position;
    }


}
