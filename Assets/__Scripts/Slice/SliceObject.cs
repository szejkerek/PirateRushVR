using UnityEngine;
using EzySlice;
using Valve.VR.InteractionSystem;

public class SliceObject : MonoBehaviour
{
    [SerializeField] Transform startSlicePoint;
    [SerializeField] Transform endSlicePoint;
    [SerializeField] LayerMask sliceableLayer;
    [SerializeField] VelocityEstimator endPointVelocity;



    public Material crossSectionMaterial;
    public float cutForce = 2000f;

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

        GameObject upperHull = hull.CreateUpperHull(target, crossSectionMaterial);
        SetUpHull(upperHull);

        GameObject lowerHull = hull.CreateLowerHull(target, crossSectionMaterial);
        SetUpHull(lowerHull);

        Destroy(target);
    }

    private void SetUpHull(GameObject hull)
    {
        Rigidbody rb = hull.AddComponent<Rigidbody>();
        MeshCollider collider = hull.AddComponent<MeshCollider>();
        collider.convex = true;
        rb.AddExplosionForce(cutForce, hull.transform.position, 1);
    }
}
