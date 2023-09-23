using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody rb;
    private ConstantForce cForce;
    public float Gravity => cForce.force.y;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        cForce = GetComponent<ConstantForce>();
    }

    public void SetVelocity(Vector3 velocity)
    {
        rb.velocity = velocity;
    }

    public void SetGravity(float value)
    {
        cForce.force = new Vector3(0, value + Physics.gravity.y, 0);
    }
}
