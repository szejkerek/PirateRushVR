using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the movement and behavior of a shark along defined waypoints.
/// </summary>
public class SharkController : MonoBehaviour
{
    [SerializeField] Transform shark;
    public List<Transform> waypoints; // Define waypoints in Unity Editor by creating empty game objects and assigning their transforms here
    public float speed = 2.0f;
    public float rotationSpeed = 2.0f;
    public float wiggleFactor = 0.2f; // Adjust the wiggle factor as needed

    private int currentWaypoint = 0;

    /// <summary>
    /// Updates the shark's movement and behavior.
    /// </summary>
    void Update()
    {
        MoveTowardsWaypoint();
        Wiggle();
    }

    /// <summary>
    /// Moves the shark towards the defined waypoints.
    /// </summary>
    void MoveTowardsWaypoint()
    {
        Vector3 targetDirection = waypoints[currentWaypoint].position - shark.position;
        float singleStep = rotationSpeed * Time.deltaTime;

        Vector3 newDirection = Vector3.RotateTowards(shark.forward, targetDirection, singleStep, 0.0f);
        shark.rotation = Quaternion.LookRotation(newDirection);

        shark.position = Vector3.MoveTowards(shark.position, waypoints[currentWaypoint].position, speed * Time.deltaTime);

        if (Vector3.Distance(shark.position, waypoints[currentWaypoint].position) < 0.1f)
        {
            currentWaypoint++;
            if (currentWaypoint >= waypoints.Count)
                currentWaypoint = 0;
        }
    }

    /// <summary>
    /// Applies a wiggling motion to the shark.
    /// </summary>
    void Wiggle()
    {
        float wiggleAmount = Mathf.PerlinNoise(Time.time, 0) * wiggleFactor; // Use Perlin noise for smooth oscillation

        Vector3 wiggleDirection = shark.right * Mathf.Sin(wiggleAmount); // Wiggle along the right direction of the shark
        shark.position += wiggleDirection * Time.deltaTime;

        float rotationAmount = Mathf.PerlinNoise(0, Time.time) * wiggleFactor * 30; // Adjust rotation intensity
        shark.Rotate(Vector3.up, rotationAmount * Time.deltaTime); // Apply rotation around the up axis of the shark
    }

}
