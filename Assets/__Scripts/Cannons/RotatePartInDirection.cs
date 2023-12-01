using UnityEngine;

public class RotatePart : MonoBehaviour
{
    [SerializeField] bool x;
    [SerializeField] float offsetX;
    [SerializeField] bool y;
    [SerializeField] float offsetY;
    [SerializeField] bool z;
    [SerializeField] float offsetZ;

    Quaternion _desiredRotation = Quaternion.identity;

    public void Rotate(Vector3 direction, float smoothSpeed)
    {
        Vector3 targetRotation = Quaternion.LookRotation(direction).eulerAngles;
        _desiredRotation = Quaternion.Euler(new Vector3(
            x ? targetRotation.x + offsetX : transform.rotation.eulerAngles.x,
            y ? targetRotation.y + offsetY : transform.rotation.eulerAngles.y, 
            z ? targetRotation.z + offsetZ : transform.rotation.eulerAngles.z
            ));

        transform.rotation = Quaternion.Lerp(transform.rotation, _desiredRotation, smoothSpeed * Time.deltaTime);
    }
    public bool IsAimedAtTarget()
    {
        return Quaternion.Angle(transform.rotation, _desiredRotation) <= 0.1f;
    }
}
