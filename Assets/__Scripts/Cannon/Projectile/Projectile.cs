using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileSO Data => data;
    private ProjectileSO data;

    private Rigidbody rb;
    private ConstantForce cForce;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cForce = GetComponent<ConstantForce>();
    }

    public void Init(ProjectileSO data, Vector3 velocity, float gravity)
    {
        this.data = data;
        rb.velocity = velocity;
        cForce.force = new Vector3(0, gravity - Physics.gravity.y, 0);
    }

    public Material GetCrossSectionMaterial()
    {
        if (data.CrossSectionMaterial == null)
        {
            Debug.LogWarning($"{gameObject.name} has no CrossSection material selected!");
            Material redMaterial = new Material(Shader.Find("Standard"));
            redMaterial.color = Color.red;
            return redMaterial;
        }

        return data.CrossSectionMaterial;
    }

    public void ApplyEffects(bool critical)
    {
        data.NormalEffects.ForEach(e => e.ApplyHitEffect(this));

        if(critical)
        {
            data.CriticalEffect.ApplyHitEffect(this);
        }

    }
}
