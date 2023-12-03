using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayerTarget : MonoBehaviour
{
    [SerializeField] Transform cameraOffset;
    [SerializeField] Transform target;

    [SerializeField, Range(0, 5f)] float heightAboveOffset;

    void Update()
    {
        Vector3 offsetPosition = cameraOffset.position + Vector3.up * heightAboveOffset;
        target.position = new Vector3(offsetPosition.x, heightAboveOffset, offsetPosition.z);
    }
}
