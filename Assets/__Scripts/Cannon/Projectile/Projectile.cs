using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Material CrossSectionMaterial => crossSectionMaterial;
    [SerializeField] Material crossSectionMaterial;

    private Rigidbody rb;
    private ConstantForce cForce;
    List<IEffect> effects;

    private void Awake()
    {
        effects = new List<IEffect>();
        rb = GetComponent<Rigidbody>();
        cForce = GetComponent<ConstantForce>();
    }

    private void Start()
    {
        effects.ForEach(e => e.ApplyStartEffect());
    }

    public void SetEffects(List<IEffect> effects)
    {
        this.effects = effects;
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    public void SetGravity(float value)
    {
        cForce.force = new Vector3(0, value - Physics.gravity.y, 0);
    }

    public void SetCrossSectionMaterial(Material material)
    {
        if (material != null)
        {
            crossSectionMaterial = material;
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} has no CrossSection material selected!");
            Material redMaterial = new Material(Shader.Find("Standard"));
            redMaterial.color = Color.red;
            crossSectionMaterial = redMaterial;
        }
    }

    public void OnSliced(bool isPerfect)
    {
        effects.ForEach(e => e.ApplySlicedEffect());

        if(isPerfect)
        {
            Debug.Log("Perfect Slice");
            AudioManager.Instance.PlayGlobal(AudioManager.Instance.SFXLib.PerfectSlice);
        }
        else
        {
            Debug.Log("Normal Slice");
        }
    }
}
