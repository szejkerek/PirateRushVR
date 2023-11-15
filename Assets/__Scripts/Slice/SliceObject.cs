using DG.Tweening;
using EzySlice;
using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class SliceObject : MonoBehaviour
{
    [SerializeField] Transform startSlicePoint;
    [SerializeField] Transform endSlicePoint;
    [SerializeField] VelocityEstimator endPointVelocity;
    [Space]
    [SerializeField] LayerMask sliceableLayer;
    [SerializeField] float cutForce = 2000f;

    float perfectSliceTolerance;
    private void Start()
    {
        perfectSliceTolerance = Systems.Instance.difficultyLevel.PerfectSliceTolerance;
    }

    private void FixedUpdate()
    {
        bool hasHit = Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out RaycastHit hit, sliceableLayer);
        if (hasHit)
        {
            GameObject target = hit.transform.gameObject;
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        if (target == null)
            return;

        Vector3 velocity = endPointVelocity.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity).normalized;

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if (hull == null)
            return;

        Projectile projectile = target.GetComponent<Projectile>();

        GameObject upperHull = hull.CreateUpperHull(target, projectile.CrossSectionMaterial);
        SetUpHull(upperHull);

        GameObject lowerHull = hull.CreateLowerHull(target, projectile.CrossSectionMaterial);
        SetUpHull(lowerHull);

        projectile.OnSliced(IsSlicePerfect(target, upperHull, lowerHull));

        Destroy(target);
    }

    private void SetUpHull(GameObject hull)
    {
        Rigidbody rb = hull.AddComponent<Rigidbody>();
        MeshCollider collider = hull.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, hull.transform.position, 1);

        StartCoroutine(DisappearAfterDelay(hull, 3, 0.65f));
    }

    private IEnumerator DisappearAfterDelay(GameObject obj, float delay, float animationTime)
    {
        yield return new WaitForSeconds(delay);
        obj.transform.DOScale(Vector3.zero, animationTime);
        Destroy(obj, animationTime);
    }

    private float CalculateVolume(Collider collider)
    {
        Bounds bounds = collider.bounds;
        return bounds.size.x * bounds.size.y * bounds.size.z;
    }

    private bool IsSlicePerfect(GameObject target, GameObject upperHull, GameObject lowerHull)
    {
        float totalVolume = CalculateVolume(target.GetComponent<Collider>());
        float upperVolume = CalculateVolume(upperHull.GetComponent<Collider>());
        float lowerVolume = CalculateVolume(lowerHull.GetComponent<Collider>());

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