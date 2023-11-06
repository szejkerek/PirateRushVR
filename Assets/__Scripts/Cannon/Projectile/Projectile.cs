using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    private ConstantForce cForce;
    public float Gravity => cForce.force.y;
    List<IEffect> effects;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cForce = GetComponent<ConstantForce>();
    }

    private void Start()
    {
        ApplyEffects();
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

    public void ApplyEffects()
    {
        if (effects.Count == 0)
            return;

        foreach (var effect in effects)
        {
            effect.ApplyEffect();
        }
    }
}
