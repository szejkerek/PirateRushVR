using DG.Tweening;
using EzySlice;
using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Katana : Weapon
{
    /// <summary>
    /// Sound for successful slice.
    /// </summary>
    [SerializeField] Sound sliceSound;

    /// <summary>
    /// Sound for hitting metal.
    /// </summary>
    [SerializeField] Sound metalHitSound;

    /// <summary>
    /// Cooldown duration for metal hit sound.
    /// </summary>
    [SerializeField] float metalHitCooldown;

    /// <summary>
    /// Time of the last sound effect.
    /// </summary>
    float lastSound = 0;

    /// <summary>
    /// Effect for sparking.
    /// </summary>
    [SerializeField] GameObject sparksEffect;

    /// <summary>
    /// Effect for splashing.
    /// </summary>
    [SerializeField] GameObject splashEffect;

    [Space]

    /// <summary>
    /// Starting point for slicing.
    /// </summary>
    [SerializeField] Transform startSlicePoint;

    /// <summary>
    /// Ending point for slicing.
    /// </summary>
    [SerializeField] Transform endSlicePoint;

    /// <summary>
    /// Velocity estimator for the endpoint.
    /// </summary>
    [SerializeField] VelocityEstimator endPointVelocity;

    [Space]

    /// <summary>
    /// Force applied during cutting.
    /// </summary>
    [SerializeField] float cutForce = 2000f;

    float perfectSliceTolerance;

    /// <summary>
    /// Initializes perfect slice tolerance based on the difficulty level.
    /// </summary>
    private void Start()
    {
        perfectSliceTolerance = Systems.Instance.difficultyLevel.PerfectSliceTolerance;
    }

    /// <summary>
    /// Defines behavior upon shooting.
    /// </summary>
    /// <param name="projectile">The projectile fired.</param>
    /// <param name="point">The point of impact.</param>
    protected override void ShootableBehavior(Projectile projectile, Vector3 point)
    {

        Vector3 velocity = endPointVelocity.GetVelocityEstimate();
        Vector3 sparksDirection = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity).normalized;
        var effect = Instantiate(sparksEffect, point, Quaternion.LookRotation(sparksDirection));
        if(Time.time - lastSound >= metalHitCooldown) 
        {
            lastSound = Time.time;
            AudioManager.Instance.PlayAtPosition(point, metalHitSound);
        }
        
        Destroy(effect, 2f);
        projectile.ApplyPoints(negative: true);
    }

    /// <summary>
    /// Determines if a hit occurred.
    /// </summary>
    /// <param name="projectile">The projectile hit.</param>
    /// <param name="point">The point of impact.</param>
    /// <param name="layerMask">Layer mask for detection.</param>
    /// <returns>True if hit; otherwise, false.</returns>
    protected override bool DidHit(out Projectile projectile, out Vector3 point, int layerMask)
    {
       projectile = null;
       point = Vector3.zero;
       if( Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit info, layerMask))
       {
            projectile = info.transform.GetComponent<Projectile>();
            point = info.point;
            return projectile != null;
       }
       return false;
    }

    /// <summary>
    /// Defines behavior upon slicing.
    /// </summary>
    /// <param name="projectile">The projectile being sliced.</param>
    /// <param name="point">The point of slicing.</param>
    protected override void SliceableBehavior(Projectile projectile, Vector3 point) 
    {
        if (projectile == null)
            return;

        Vector3 velocity = endPointVelocity.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity).normalized;

        SlicedHull hull = projectile.gameObject.Slice(endSlicePoint.position, planeNormal);

        if (hull == null)
            return;

        Material mat = projectile.GetCrossSectionMaterial();

        GameObject upperHull = hull.CreateUpperHull(projectile.gameObject, mat);
        SetUpHull(upperHull);

        GameObject lowerHull = hull.CreateLowerHull(projectile.gameObject, mat);
        SetUpHull(lowerHull);

        bool isPerfect = IsSlicePerfect(upperHull, lowerHull);
        projectile.ApplyEffects(isPerfect);
        projectile.ApplyPoints(false, isPerfect);


        SplashEffect effect = Instantiate(splashEffect, point, Quaternion.LookRotation(planeNormal)).GetComponent<SplashEffect>();
        effect.Init(projectile.Data.OptionalData.SliceParticleEffectColor);

        AudioManager.Instance.PlayOnTarget(gameObject, sliceSound);

        Destroy(effect.gameObject, 3f);
        Destroy(projectile.gameObject);
    }

    /// <summary>
    /// Sets up the hull after slicing.
    /// </summary>
    /// <param name="hull">The sliced hull object.</param>
    private void SetUpHull(GameObject hull)
    {
        Rigidbody rb = hull.AddComponent<Rigidbody>();
        MeshCollider collider = hull.AddComponent<MeshCollider>();
        rb.gameObject.layer = LayerMask.NameToLayer("ProjectileNonCollision");
        collider.convex = true;
        rb.AddExplosionForce(cutForce, hull.transform.position, 1);

        StartCoroutine(DisappearAfterDelay(hull, 2, 0.25f));
    }

    /// <summary>
    /// Coroutine to make an object disappear after a delay.
    /// </summary>
    /// <param name="obj">The object to disappear.</param>
    /// <param name="delay">The delay before disappearance.</param>
    /// <param name="animationTime">The time taken for the disappearance animation.</param>
    private IEnumerator DisappearAfterDelay(GameObject obj, float delay, float animationTime)
    {
        yield return new WaitForSeconds(delay);
        obj.transform.DOScale(Vector3.zero, animationTime);
        Destroy(obj, animationTime + 0.2f);
    }

    /// <summary>
    /// Calculates the volume of a mesh collider.
    /// </summary>
    /// <param name="collider">The mesh collider.</param>
    /// <returns>The calculated volume.</returns>
    private float CalculateVolume(MeshCollider collider)
    {
        Mesh mesh = collider.sharedMesh;
        if (mesh != null)
        {
            Vector3[] vertices = mesh.vertices;
            int[] triangles = mesh.triangles;

            float volume = 0f;
            for (int i = 0; i < triangles.Length; i += 3)
            {
                Vector3 v1 = vertices[triangles[i]];
                Vector3 v2 = vertices[triangles[i + 1]];
                Vector3 v3 = vertices[triangles[i + 2]];

                volume += SignedVolumeOfTriangle(v1, v2, v3);
            }
            return Mathf.Abs(volume);
        }
      
        return 0f;
    }

    /// <summary>
    /// Calculates the signed volume of a triangle.
    /// </summary>
    /// <param name="p1">Point 1 of the triangle.</param>
    /// <param name="p2">Point 2 of the triangle.</param>
    /// <param name="p3">Point 3 of the triangle.</param>
    /// <returns>The signed volume of the triangle.</returns>
    private float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return Vector3.Dot(Vector3.Cross(p1, p2), p3) / 6f;
    }

    /// <summary>
    /// Checks if the slice is perfect.
    /// </summary>
    /// <param name="upperHull">The upper sliced hull object.</param>
    /// <param name="lowerHull">The lower sliced hull object.</param>
    /// <returns>True if the slice is perfect; otherwise, false.</returns>
    private bool IsSlicePerfect(GameObject upperHull, GameObject lowerHull)
    {
        float upperVolume = CalculateVolume(upperHull.GetComponent<MeshCollider>());
        float lowerVolume = CalculateVolume(lowerHull.GetComponent<MeshCollider>());
        float totalVolume = upperVolume + lowerVolume;

        float upperRatio = upperVolume / totalVolume;
        float lowerRatio = lowerVolume / totalVolume;

        return IsWithinPerfectSlice(upperRatio) && IsWithinPerfectSlice(lowerRatio);
    }

    /// <summary>
    /// Checks if a value is within the perfect slice range.
    /// </summary>
    /// <param name="value">The value to check.</param>
    /// <returns>True if the value is within the perfect slice range; otherwise, false.</returns>
    bool IsWithinPerfectSlice(float value)
    {
        float lowerBound = 0.5f - perfectSliceTolerance;
        float upperBound = 0.5f + perfectSliceTolerance;
        return (value >= lowerBound && value <= upperBound);
    }
}