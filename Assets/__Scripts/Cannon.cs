using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Projectile GoodBullet;
    public Projectile BadBullet;

    [SerializeField] private Transform target;

    private ShootObjectParabolic luncher;

    private void Awake()
    {
        luncher = GetComponent<ShootObjectParabolic>();
    }

    private void Start()
    {
        luncher.LunchProjectile(target, BadBullet);
    }
}
