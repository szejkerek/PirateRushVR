using UnityEngine;
using EzySlice;

public class SliceObject : MonoBehaviour
{
    public Transform planeDebug;
    public GameObject target;
    public Material crossSectionMaterial;
    public float cutForce = 2000f;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        if (target == null)
            return;

        SlicedHull hull = target.Slice(planeDebug.position, planeDebug.up);

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
