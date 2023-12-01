using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    [SerializeField] GameObject fireEffect;

    private void Update()
    {
        fireEffect.transform.rotation = Quaternion.LookRotation(Vector3.up, transform.up);
    }
}
