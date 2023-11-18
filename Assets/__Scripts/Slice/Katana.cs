using DG.Tweening;
using EzySlice;
using System;
using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class Katana : MonoBehaviour
{
    [SerializeField] Transform startSlicePoint;
    [SerializeField] Transform endSlicePoint;
    [SerializeField] VelocityEstimator endPointVelocity;
    [Space]
    [SerializeField] LayerMask sliceableLayer;
    [SerializeField] LayerMask shootableLayer;
    [SerializeField] LayerMask collectibleLayer;
    [SerializeField] float cutForce = 2000f;

    float perfectSliceTolerance;
    private void Start()
    {
        perfectSliceTolerance = Systems.Instance.difficultyLevel.PerfectSliceTolerance;
    }

    private void FixedUpdate()
    {
        if(DidHit(out RaycastHit hitSliceable, sliceableLayer))
        {
            Slice(hitSliceable.transform.gameObject);
        }
        else if(DidHit(out RaycastHit hitShootable, shootableLayer))
        {
            ShootBehaviour(hitShootable.transform.gameObject);
        }
        else
        {

        }
    }

    private void ShootBehaviour(GameObject gameObject)
    {
        
    }

    private bool DidHit(out RaycastHit hit, int layerMask)
    {
       return Physics.Linecast(startSlicePoint.position, endSlicePoint.position, out hit, layerMask);
    }

    private void Slice(GameObject target)
    {
        if (target == null)
            return;

        Vector3 velocity = endPointVelocity.GetVelocityEstimate();
        Vector3 planeNormal = Vector3.Cross(endSlicePoint.position - startSlicePoint.position, velocity).normalized;

        SlicedHull hull = target.Slice(endSlicePoint.position, planeNormal);

        if (hull == null)
            return;

        Projectile projectile = target.GetComponent<Projectile>();

        Material mat = projectile.Data.CrossSectionMaterial;

        GameObject upperHull = hull.CreateUpperHull(target, mat);
        SetUpHull(upperHull);

        GameObject lowerHull = hull.CreateLowerHull(target, mat);
        SetUpHull(lowerHull);

        projectile.ApplyEffects(IsSlicePerfect(upperHull, lowerHull));

        Destroy(target);
    }

    private void SetUpHull(GameObject hull)
    {
        Rigidbody rb = hull.AddComponent<Rigidbody>();
        MeshCollider collider = hull.AddComponent<MeshCollider>();
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
        //if (collider is MeshCollider meshCollider)
        //{
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
        //}
        //}
        //else if (collider is BoxCollider boxCollider)
        //{
        //    return boxCollider.size.x * boxCollider.size.y * boxCollider.size.z;
        //}
        //else if (collider is SphereCollider sphereCollider)
        //{
        //    return (4f / 3f) * Mathf.PI * Mathf.Pow(sphereCollider.radius, 3);
        //}
        //else if (collider is CapsuleCollider capsuleCollider)
        //{
        //    float radius = capsuleCollider.radius;
        //    float height = capsuleCollider.height;

        //    float cylinderVolume = Mathf.PI * Mathf.Pow(radius, 2) * height;
        //    float sphereVolume = (4f / 3f) * Mathf.PI * Mathf.Pow(radius, 3);

        //    return cylinderVolume + sphereVolume;
        //}
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