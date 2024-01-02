using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves a target transform to a position above a specified camera offset.
/// </summary>
public class MovePlayerTarget : MonoBehaviour
{
    [SerializeField] Transform cameraOffset;
    [SerializeField] Transform target;

    [SerializeField, Range(0, 5f)] float heightAboveOffset;

    /// <summary>
    /// Updates the position of the target based on the camera offset.
    /// </summary>
    void Update()
    {
        // Calculate the offset position with a specified height above the camera offset
        Vector3 offsetPosition = cameraOffset.position + Vector3.up * heightAboveOffset;

        // Set the target's position to be aligned with the calculated offset position at a specific height
        target.position = new Vector3(offsetPosition.x, heightAboveOffset, offsetPosition.z);
    }
}
