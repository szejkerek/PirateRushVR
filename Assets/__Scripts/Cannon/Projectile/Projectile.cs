using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public ProjectileSO Data => data;
    private ProjectileSO data;

    private Rigidbody rb;
    private ConstantForce cForce;
    List<IEffect> effects;

    private void Awake()
    {
        effects = new List<IEffect>();
        rb = GetComponent<Rigidbody>();
        cForce = GetComponent<ConstantForce>();
    }

    public void Init(ProjectileSO data, Vector3 velocity, float gravity)
    {
        this.data = data;
        rb.velocity = velocity;
        cForce.force = new Vector3(0, gravity - Physics.gravity.y, 0);
    }

    private void Start()
    {
        effects.ForEach(e => e.ApplyStartEffect());
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

    public void OnSliced(bool isPerfect)
    {
        effects.ForEach(e => e.ApplySlicedEffect());

        if(isPerfect)
        {
            AudioManager.Instance.PlayGlobal(AudioManager.Instance.SFXLib.PerfectSlice);
            Debug.Log("Perfect Slice");
        }
    }
}
