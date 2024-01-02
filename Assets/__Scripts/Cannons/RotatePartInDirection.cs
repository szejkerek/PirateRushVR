using UnityEngine;

/// <summary>
/// Class handling the rotation of a part in three dimensions.
/// </summary>
public class RotatePart : MonoBehaviour
{
    [SerializeField] bool x;
    [SerializeField] float offsetX;
    [SerializeField] bool y;
    [SerializeField] float offsetY;
    [SerializeField] bool z;
    [SerializeField] float offsetZ;

    Quaternion _desiredRotation = Quaternion.identity;

    /// <summary>
    /// Rotates the part towards a specified direction with a smooth effect.
    /// </summary>
    /// <param name="direction">The direction to rotate towards.</param>
    /// <param name="smoothSpeed">The speed of rotation smoothing.</param>
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

    /// <summary>
    /// Checks if the part is aimed precisely at its desired rotation.
    /// </summary>
    /// <returns>True if the part is aimed correctly, false otherwise.</returns>
    public bool IsAimedAtTarget()
    {
        return Quaternion.Angle(transform.rotation, _desiredRotation) <= 0.1f;
    }
}
