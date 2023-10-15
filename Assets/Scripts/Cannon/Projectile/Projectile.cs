using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    private ConstantForce cForce;
    public float Gravity => cForce.force.y;
    IEffect[] effects;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cForce = GetComponent<ConstantForce>();
        effects = GetComponents<IEffect>();
    }

    private void Start()
    {
        ApplyEffects();
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
        if (effects.Length == 0)
        {
            Debug.Log("Projectile has no effects!");
            return;
        }

        foreach (var effect in effects)
        {
            effect.ApplyEffect();
        }
    }
}
