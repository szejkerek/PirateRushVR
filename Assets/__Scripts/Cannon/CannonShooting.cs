using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CannonShooting : MonoBehaviour
{
    public CannonSettings Settings => settings;
    [SerializeField] CannonSettings settings;

    [SerializeField] private Transform shootingPoint;

    Transform target;
    Vector3 targetDirection;
    RotatePart[] rotateParts;

    private void Start()
    {
        SetTarget("Player");
        rotateParts = GetComponentsInChildren<RotatePart>();
    }

    private void Update()
    {
        RotateTower(targetDirection);
    }

    private void RotateTower(Vector3 dir)
    {
        foreach (var rotatePart in rotateParts)
        {
            rotatePart.Rotate(dir.normalized, settings.RotationSmoothing);
        }
    }

    private void SetTarget(string tag)
    {
        target = GameObject.FindGameObjectWithTag(tag).transform;
        targetDirection = CalculateDirection(target.position);
    }

    public void Shoot(ProjectileSO projectile)
    {
        float gravity = settings.Gravity.GetValueBetween(); ;
        Vector3 diffusedTargetPosition = CalculateRandomTargetPosition(target);
        targetDirection = CalculateDirection(diffusedTargetPosition, gravity);    

        InitProjectile(projectile, targetDirection, gravity);
    }

    private void InitProjectile(ProjectileSO data, Vector3 direction, float gravity)
    {
        GameObject obj = Instantiate(data.Model, shootingPoint.position, shootingPoint.rotation);
        obj.AddComponent<ConstantForce>();
        //obj.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        Projectile projectile = obj.AddComponent<Projectile>();
        projectile.Init(data, direction, gravity);
     }

    private Vector3 CalculateRandomTargetPosition(Transform player)
    {
        float randomAngle = Random.Range(0f, 360f);
        float randomDistance = Random.Range(0f, Mathf.Abs(settings.RandomTargetRange));

        Vector3 offset = new Vector3(
            Mathf.Cos(randomAngle * Mathf.Deg2Rad) * randomDistance,
            0f,
            Mathf.Sin(randomAngle * Mathf.Deg2Rad) * randomDistance
        );

        return player.position + offset;
    }

    public Vector3 CalculateDirection(Vector3 target, float gravity = -9.81f)
    {
        float height = settings.Height.GetValueBetween();
        
        float displacementY = target.y - shootingPoint.position.y;
        Vector3 displacementXZ = new Vector3(target.x - shootingPoint.position.x, 0, target.z - shootingPoint.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity));


        return velocityY + velocityXZ;
    }


}
