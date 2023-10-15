using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Projectile GoodBullet;
    public Projectile BadBullet;

    [SerializeField] private Transform target;

    private RotatePart[] rotateParts;
    private ShootObjectParabolic luncher;

    private void Awake()
    {
        luncher = GetComponent<ShootObjectParabolic>();
        rotateParts = GetComponentsInChildren<RotatePart>();
    }


    private void Update()
    {
        foreach (var rotatePart in rotateParts)
        {
            rotatePart.Rotate(luncher.CalculateDirection(target.position));
        }

        if (Input.GetKey(KeyCode.Mouse0))
            Shoot();
    }

    private void Shoot()
    {
       // if (!rotate.IsLockedOnTarget())
            //return;
        luncher.LunchProjectile(target.position, BadBullet);
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
