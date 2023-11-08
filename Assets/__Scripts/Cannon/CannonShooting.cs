using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CannonShooting : MonoBehaviour
{
    public List<ProjectileSO> GoodBullets => goodBullets;
    List<ProjectileSO> goodBullets;

    public List<ProjectileSO> BadBullets => badBullets;
    List<ProjectileSO> badBullets;

    public List<ProjectileSO> SpecialBullets => specialBullets;
    List<ProjectileSO> specialBullets;

    [SerializeField] private Transform shootingPoint;

    public Transform Target => target;
    Transform target;

    CannonSettings settings;

    private void Start()
    {
        settings = CannonsManager.Instance.CannonSettings;
        DataLoader<ProjectileSO> dataLoader = new DataLoader<ProjectileSO>();
        goodBullets = dataLoader.Load(settings.GoodBulletLabel);
        badBullets = dataLoader.Load(settings.BadBulletLabel);
        specialBullets = dataLoader.Load(settings.SpecialBulletLabel);
    }

    public void SetTarget(Transform target)
    {
        this.target = target;   
    }

    public void Shoot(ProjectileSO projectile)
    {
        Vector3 diffusedTargetPosition = CalculateTargerPosition(target);
        Vector3 direction = CalculateDirection(diffusedTargetPosition);

        SetupProjectile(projectile, direction);

    }

    private void SetupProjectile(ProjectileSO data, Vector3 direction)
    {
        GameObject obj = Instantiate(data.Model, shootingPoint.position, shootingPoint.rotation);
        obj.AddComponent<ConstantForce>();
        Projectile projectile = obj.AddComponent<Projectile>();
        projectile.SetGravity(settings.Gravity);
        projectile.SetVelocity(direction);
        projectile.SetEffects(data.Effects.ToList<IEffect>());
    }

    private Vector3 CalculateTargerPosition(Transform target)
    {
        return target.position;
    }

    public Vector3 CalculateDirection(Vector3 target)
    {
        float displacementY = target.y - shootingPoint.position.y;
        Vector3 displacementXZ = new Vector3(target.x - shootingPoint.position.x, 0, target.z - shootingPoint.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * settings.Gravity * settings.Height);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * settings.Height / settings.Gravity) + Mathf.Sqrt(2 * (displacementY - settings.Height) / settings.Gravity));


        return velocityY + velocityXZ;
    }


}
