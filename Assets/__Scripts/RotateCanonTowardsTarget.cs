using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCanonTowardsTarget : MonoBehaviour
{
    [SerializeField] Transform canonBase;
    [SerializeField] Transform canonRifle;

    [SerializeField] float baseSpeed;
    [SerializeField] float rifleSpeed;
    [SerializeField] float rotationTolerance;

    bool baseLockedOnTarget = false;
    bool rifleLockedOnTarget = false;
    ShootObjectParabolic luncher;

    private void Awake()
    {
        luncher = GetComponent<ShootObjectParabolic>();
    }

    public void Rotate(Transform target)
    {
        //if (baseLockedOnTarget && rifleLockedOnTarget)
        //{
        //    return;
        //}
        Quaternion targetRotation = Quaternion.LookRotation(luncher.CalculateDirection(target));
        baseLockedOnTarget = RotateBase(targetRotation);
        rifleLockedOnTarget = RotateRifle(targetRotation);
    }

    private bool RotateBase(Quaternion targetRotation)
    {
        Vector3 eulerRotation = targetRotation.eulerAngles;
        Quaternion desiredRotation = Quaternion.Euler(new Vector3(canonBase.rotation.eulerAngles.x, eulerRotation.y, canonBase.rotation.eulerAngles.z));

        canonBase.rotation = Quaternion.Lerp(canonBase.rotation, desiredRotation, baseSpeed * Time.deltaTime);
        return isAimingAtTarget(canonBase.rotation, desiredRotation);
    }

    private bool RotateRifle(Quaternion targetRotation)
    {
        Vector3 eulerRotation = targetRotation.eulerAngles;
        Quaternion desiredRotation = Quaternion.Euler(new Vector3(eulerRotation.x, canonRifle.rotation.eulerAngles.y, canonRifle.rotation.eulerAngles.z));

        canonRifle.rotation = Quaternion.Lerp(canonRifle.rotation, desiredRotation, rifleSpeed * Time.deltaTime);
        return isAimingAtTarget(canonRifle.rotation, desiredRotation);
    }

    private bool isAimingAtTarget(Quaternion currentRotation, Quaternion desiredRotation)
    {
        return Quaternion.Angle(currentRotation, desiredRotation) <= rotationTolerance;
    }

}
