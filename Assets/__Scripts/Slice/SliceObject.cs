using UnityEngine;
using EzySlice;
using Valve.VR.InteractionSystem;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public class SliceObject : MonoBehaviour
{
    [SerializeField] Transform startSlicePoint;
    [SerializeField] Transform endSlicePoint;
    [SerializeField] LayerMask sliceableLayer;
    [SerializeField] VelocityEstimator endPointVelocity;
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

        if (target.TryGetComponent(out Projectile projectile))
        {
            Vector3 velocity = endPointVelocity.GetVelocityEstimate();
            Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity).normalized;

            SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

            if (hull == null)
                return;

            GameObject upperHull = hull.CreateUpperHull(target, projectile.CrossSectionMaterial);
            SetUpHull(upperHull);

            GameObject lowerHull = hull.CreateLowerHull(target, projectile.CrossSectionMaterial);
            SetUpHull(lowerHull);
        }

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
