using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// ScriptableObject holding settings for the Cannon.
/// </summary>
[CreateAssetMenu(fileName = "CannonSettings", menuName = "Cannon/CannonSettings", order = 1)]
public class CannonSettings : ScriptableObject
{
    /// <summary>
    /// List of available projectiles.
    /// </summary>
    [field: SerializeField]
    public List<ProjectileSO> Projectiles { private set; get; }

    /// <summary>
    /// Random range for target shoot point.
    /// </summary>
    [field: Header("Settings")]
    [field: SerializeField]
    public float RandomTargetRange { private set; get; }

    /// <summary>
    /// Smoothing value for rotation.
    /// </summary>
    [field: SerializeField]
    public float RotationSmoothing { private set; get; }

    /// <summary>
    /// Interval for defining height.
    /// </summary>
    [field: SerializeField]
    public Interval<float> Height { private set; get; }

    /// <summary>
    /// Interval for defining gravity.
    /// </summary>
    [field: SerializeField]
    public Interval<float> Gravity { private set; get; }
}
