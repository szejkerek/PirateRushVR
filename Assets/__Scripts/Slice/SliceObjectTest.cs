using UnityEngine;
using EzySlice;
using Valve.VR.InteractionSystem;
using DG.Tweening;
using System.Collections.Generic;
using System.Collections;

public class SliceObjectTest : MonoBehaviour
{
    [Header("Test")]
    public Transform plane;
    public GameObject target;

    [Space]
    [SerializeField] Material material;
    [SerializeField] LayerMask sliceableLayer;
    [SerializeField] float cutForce = 2000f;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Slice(target);
        }
    }

    public void Slice(GameObject target)
    {
        if (target == null)
            return;

        SlicedHull hull = target.Slice(plane.position, plane.up);
        Projectile projectile = target.GetComponent<Projectile>();

        if (hull == null)
            return;

        Material mat = projectile.Data.CrossSectionMaterial;

        GameObject upperHull = hull.CreateUpperHull(target, mat);
        SetUpHull(upperHull);

        GameObject lowerHull = hull.CreateLowerHull(target, mat);
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
