using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootObjectParabolic : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float height;
    [SerializeField] private float gravity;
    [Space, SerializeField] private Transform shootingPoint;

    public void LunchProjectile(Vector3 target, Projectile projectile)
    {
        Vector3 direction = CalculateDirection(target);

        Projectile obj = Instantiate(projectile, shootingPoint);

        obj.SetGravity(gravity);
        obj.SetVelocity(direction);
    }

    public Vector3 CalculateDirection(Vector3 target)
    {
        float displacementY = target.y - shootingPoint.position.y;
        Vector3 displacementXZ = new Vector3(target.x - shootingPoint.position.x, 0, target.z - shootingPoint.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity));


        return velocityY + velocityXZ;
    }


}
