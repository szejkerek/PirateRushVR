using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Projectile GoodBullet;
    public Projectile BadBullet;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private Transform target;

    private ShootObjectParabolic luncher;
    private RotateCanonTowardsTarget rotate;

    private void Awake()
    {
        luncher = GetComponent<ShootObjectParabolic>();
        rotate = GetComponent<RotateCanonTowardsTarget>();
    }


    private void Update()
    {
        if(Input.GetKey(KeyCode.Space)) 
        {
            rotate.SetTarget(target.position);
        }

        if (Input.GetKey(KeyCode.Mouse0))
            Shoot();
    }

    private void Shoot()
    {
        if (!rotate.IsLockedOnTarget())
            return;
        luncher.LunchProjectile(target.position, BadBullet);
    }

}
