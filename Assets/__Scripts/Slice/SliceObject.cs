using UnityEngine;
using EzySlice;
using Valve.VR.InteractionSystem;
using DG.Tweening;
using System.Collections;

public class SliceObject : MonoBehaviour
{
    [SerializeField] Transform startSlicePoint;
    [SerializeField] Transform endSlicePoint;
    [SerializeField] VelocityEstimator endPointVelocity;
    [Space]
    [SerializeField] LayerMask sliceableLayer;
    [SerializeField] float cutForce = 2000f;

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
        Projectile projectile = target.GetComponent<Projectile>();
        projectile.Effects.ForEach(e => e.ApplySlicedEffect());

        if (hull == null || projectile == null)
            return;

        GameObject upperHull = hull.CreateUpperHull(target, projectile.CrossSectionMaterial);
        SetUpHull(upperHull);

        GameObject lowerHull = hull.CreateLowerHull(target, projectile.CrossSectionMaterial);
        SetUpHull(lowerHull);

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
}