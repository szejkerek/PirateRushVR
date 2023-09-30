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
        if(Input.GetKeyDown(KeyCode.Space)) 
        {
            Shoot();
        }
        rotate.Rotate(target);

    }

    private void Shoot()
    {
        luncher.LunchProjectile(target, BadBullet);

    }

}
