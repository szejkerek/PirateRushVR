using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CannonShooting : MonoBehaviour
{
    public List<Projectile> GoodBullets => goodBullets;
    List<Projectile> goodBullets;

    public List<Projectile> BadBullets => badBullets;
    List<Projectile> badBullets;

    public List<Projectile> SpecialBullets => specialBullets;
    List<Projectile> specialBullets;


    [Header("Adressables Labels")]
    [SerializeField] AssetLabelReference goodBulletLabel;
    [SerializeField] AssetLabelReference badBulletLabel;
    [SerializeField] AssetLabelReference specialBulletLabel;

    [Header("Settings")]
    [SerializeField] private float speed;
    [SerializeField] private float height;
    [SerializeField] private float gravity;
    [Space, SerializeField] private Transform shootingPoint;

    public Transform Target => target;
    Transform target;

    private void Start()
    {
        DataLoader<Projectile> dataLoader = new DataLoader<Projectile>();
        goodBullets = dataLoader.Load(goodBulletLabel);
        badBullets = dataLoader.Load(badBulletLabel);
        specialBullets = dataLoader.Load(specialBulletLabel);
    }

    private void Update()
    {
        //if (Input.GetKey(KeyCode.Mouse0))
            //Shoot(GoodBullet);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;   
    }

    public void Shoot(Projectile projectile)
    {
        Vector3 diffusedTargetPosition = CalculateTargerPosition(target);
        Vector3 direction = CalculateDirection(diffusedTargetPosition);

        Projectile obj = Instantiate(projectile, shootingPoint);

        obj.SetGravity(gravity);
        obj.SetVelocity(direction);
    }

    private Vector3 CalculateTargerPosition(Transform target)
    {
        return target.position;
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
