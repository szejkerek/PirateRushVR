using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketControl : MonoBehaviour
{
    public float speed = 5f;  // Speed of the rocket.
    public float range = 10f; // Maximum range for random movement.
    public float smoothTime = 0.5f; // Smoothing time.

    private Vector3 targetPosition;
    private Vector3 start;
    private Vector3 velocity = Vector3.zero;

    private void Start()
    {
        RandomizeTarget();
    }

    private void Update()
    {
        // Calculate the smooth movement using SmoothDamp.
        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);

        // If the rocket is close to the target position, set a new random target position.
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            RandomizeTarget();
        }
    }

    private void RandomizeTarget()
    {
        targetPosition = start + Random.insideUnitSphere * range;
        targetPosition.y = Mathf.Max(3f, targetPosition.y);
    }
}
