using System;
using UnityEngine;

[Serializable]
public class ProjectileOptionalData
{
    [field: SerializeField] public bool AlwaysNegativePoints { private set; get; }

    [field: Header("Shooting")]
    [field: SerializeField] public GameObject FracturedModel { private set; get; }
    
    [field: Header("Slicing")]
    [field: SerializeField] public Material CrossSectionMaterial { private set; get; }
    [field: SerializeField] public Color SliceParticleEffectColor { private set; get; }
}