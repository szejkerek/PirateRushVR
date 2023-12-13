using DG.Tweening;
using EzySlice;
using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Katana : Weapon
{
    [SerializeField] Sound sliceSound;
    [SerializeField] GameObject sparksEffect;
    [SerializeField] GameObject splashEffect;
    [Space]
    [SerializeField] Transform startSlicePoint;
    [SerializeField] Transform endSlicePoint;
    [SerializeField] VelocityEstimator endPointVelocity;
    [Space]
    [SerializeField] float cutForce = 2000f;

    float perfectSliceTolerance;
    private void Start()
    {
        perfectSliceTolerance = Systems.Instance.difficultyLevel.PerfectSliceTolerance;
    }

    protected override void ShootableBehavior(Projectile projectile, Vector3 point)
    {
        Vector3 velocity = endPointVelocity.GetVelocityEstimate();
        Vector3 sparksDirection = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity).normalized;
        var effect = Instantiate(sparksEffect, point, Quaternion.LookRotation(sparksDirection));
        Destroy(effect, 2f);
        projectile.ApplyPoints(negative: true);
    }

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

        AudioManager.Instance.Play(gameObject, sliceSound, SoundType.SFX);

        Destroy(effect.gameObject, 3f);
        Destroy(projectile.gameObject);
    }

    private void SetUpHull(GameObject hull)
    {
        Rigidbody rb = hull.AddComponent<Rigidbody>();
        MeshCollider collider = hull.AddComponent<MeshCollider>();
        rb.gameObject.layer = LayerMask.NameToLayer("ProjectileNonCollision");
        collider.convex = true;
        rb.AddExplosionForce(cutForce, hull.transform.position, 1);

        StartCoroutine(DisappearAfterDelay(hull, 2, 0.25f));
    }

    private IEnumerator DisappearAfterDelay(GameObject obj, float delay, float animationTime)
    {
        yield return new WaitForSeconds(delay);
        obj.transform.DOScale(Vector3.zero, animationTime);
        Destroy(obj, animationTime + 0.2f);
    }

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

    private float SignedVolumeOfTriangle(Vector3 p1, Vector3 p2, Vector3 p3)
    {
        return Vector3.Dot(Vector3.Cross(p1, p2), p3) / 6f;
    }

    private bool IsSlicePerfect(GameObject upperHull, GameObject lowerHull)
    {
        float upperVolume = CalculateVolume(upperHull.GetComponent<MeshCollider>());
        float lowerVolume = CalculateVolume(lowerHull.GetComponent<MeshCollider>());
        float totalVolume = upperVolume + lowerVolume;

        float upperRatio = upperVolume / totalVolume;
        float lowerRatio = lowerVolume / totalVolume;

        return IsWithinPerfectSlice(upperRatio) && IsWithinPerfectSlice(lowerRatio);
    }

    bool IsWithinPerfectSlice(float value)
    {
        float lowerBound = 0.5f - perfectSliceTolerance;
        float upperBound = 0.5f + perfectSliceTolerance;
        return (value >= lowerBound && value <= upperBound);
    }
}