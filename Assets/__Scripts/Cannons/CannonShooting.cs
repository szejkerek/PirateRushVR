using UnityEngine;

public class CannonShooting : MonoBehaviour
{
    public CannonSettings Settings => settings;
    [SerializeField] CannonSettings settings;

    [SerializeField] private Sound shootingSound;
    [SerializeField] private Transform shootingPoint;

    Transform target;
    Vector3 targetDirection;
    RotatePart[] rotateParts;

    private void Start()
    {
        SetTarget("Target");
        rotateParts = GetComponentsInChildren<RotatePart>();
    }

    private void Update()
    {
        RotateTower(targetDirection);
    }

    /// <summary>
    /// Rotates the tower towards the specified direction.
    /// </summary>
    /// <param name="dir">Direction to rotate towards.</param>
    private void RotateTower(Vector3 dir)
    {
        foreach (var rotatePart in rotateParts)
        {
            rotatePart.Rotate(dir.normalized, settings.RotationSmoothing);
        }
    }

    /// <summary>
    /// Sets the target object with the given tag.
    /// </summary>
    /// <param name="tag">Tag to identify the target object.</param>
    private void SetTarget(string tag)
    {
        target = GameObject.FindGameObjectWithTag(tag).transform;
        targetDirection = CalculateDirection(target.position);
    }

    /// <summary>
    /// Initiates shooting with the provided projectile settings.
    /// </summary>
    /// <param name="projectile">Projectile settings.</param>
    public void Shoot(ProjectileSO projectile)
    {
        float gravity = settings.Gravity.GetValueBetween();
        Vector3 diffusedTargetPosition = CalculateRandomTargetPosition(target);
        targetDirection = CalculateDirection(diffusedTargetPosition, gravity);

        InitProjectile(projectile, targetDirection, gravity);

        AudioManager.Instance.PlayOnTarget(gameObject, shootingSound);
    }

    /// <summary>
    /// Initializes a projectile with specified settings.
    /// </summary>
    /// <param name="data">Projectile settings.</param>
    /// <param name="direction">Direction for the projectile.</param>
    /// <param name="gravity">Gravity value for the projectile.</param>
    private void InitProjectile(ProjectileSO data, Vector3 direction, float gravity)
    {
        GameObject obj = Instantiate(data.Model, shootingPoint.position, shootingPoint.rotation);
        MeshCollider meshObj = obj.AddComponent<MeshCollider>();
        meshObj.convex = true;
        obj.layer = LayerMask.NameToLayer("Projectile");

        AddMeshCollidersRecursively(obj.transform);

        obj.transform.SetParent(CannonsManager.Instance.transform);
        obj.AddComponent<ConstantForce>();
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        rb.collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
        Projectile projectile = obj.AddComponent<Projectile>();
        projectile.Init(data, direction, gravity);
    }

    /// <summary>
    /// Adds MeshColliders to the GameObject and its children recursively.
    /// </summary>
    /// <param name="parentTransform">Parent transform to start adding MeshColliders.</param>
    void AddMeshCollidersRecursively(Transform parentTransform)
    {
        foreach (Transform childTransform in parentTransform)
        {
            MeshCollider mesh = childTransform.gameObject.AddComponent<MeshCollider>();
            mesh.convex = true;
            childTransform.gameObject.layer = LayerMask.NameToLayer("Projectile");

            AddMeshCollidersRecursively(childTransform);
        }
    }

    /// <summary>
    /// Calculates a random target position around the player.
    /// </summary>
    /// <param name="player">Transform of the player.</param>
    /// <returns>Random target position around the player.</returns>
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


    /// <summary>
    /// Calculates the direction for shooting the projectile.
    /// </summary>
    /// <param name="target">Target position.</param>
    /// <param name="gravity">Gravity value. Default is -9.81f.</param>
    /// <returns>The calculated direction.</returns>
    public Vector3 CalculateDirection(Vector3 target, float gravity = -9.81f)
    {
        float height = Mathf.Max(settings.Height.GetValueBetween(), target.y);

        float displacementY = target.y - shootingPoint.position.y;
        Vector3 displacementXZ = new Vector3(target.x - shootingPoint.position.x, 0, target.z - shootingPoint.position.z);

        Vector3 velocityY = Vector3.up * Mathf.Sqrt(-2 * gravity * height);
        Vector3 velocityXZ = displacementXZ / (Mathf.Sqrt(-2 * height / gravity) + Mathf.Sqrt(2 * (displacementY - height) / gravity));

        return velocityY + velocityXZ;
    }


}
