using System;
using UnityEngine;

/// <summary>
/// Serializable class containing optional data associated with a projectile.
/// </summary>
[Serializable]
public class ProjectileOptionalData
{
    /// <summary>
    /// Custom hit sound for the projectile.
    /// </summary>
    [field: SerializeField] public Sound CustomHitSound { private set; get; }

    /// <summary>
    /// Indicates if the projectile always yields negative points.
    /// </summary>
    [field: SerializeField] public bool AlwaysNegativePoints { private set; get; }

    [field: Header("Shooting")]
    /// <summary>
    /// The fractured model associated with shooting.
    /// </summary>
    [field: SerializeField] public GameObject FracturedModel { private set; get; }

    [field: Header("Slicing")]
    /// <summary>
    /// The cross-section material for slicing.
    /// </summary>
    [field: SerializeField] public Material CrossSectionMaterial { private set; get; }

    /// <summary>
    /// The color of the slice particle effect.
    /// </summary>
    [field: SerializeField] public Color SliceParticleEffectColor { private set; get; }
}
