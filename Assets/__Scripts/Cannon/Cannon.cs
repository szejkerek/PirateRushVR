using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public ComboController ComboManager => comboManager;
    ComboController comboManager;

    [SerializeField] private Transform target;

    private RotatePart[] rotateParts;
    private CannonShooting luncher;

    private void Awake()
    {
        luncher = GetComponent<CannonShooting>();
        rotateParts = GetComponentsInChildren<RotatePart>();
        comboManager = GetComponent<ComboController>();
        luncher.SetTarget(target);
    }

    private void Update()
    {
        foreach (var rotatePart in rotateParts)
        {
            rotatePart.Rotate(luncher.CalculateDirection(luncher.Target.position));
        }
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
