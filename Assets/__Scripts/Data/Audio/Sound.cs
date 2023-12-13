using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Sound", menuName = "Audio/Sound", order = 1)]
public class Sound : ScriptableObject
{
    [field: SerializeField] public AudioClip Clip { private set; get; }
    public float Volume => volume;
    [SerializeField, Range(0, 1)] float volume = 1;
    public float InitialPitch => initialPitch;
    [SerializeField, Range(-3, 3)] float initialPitch = 1;

    public bool ShouldRandomizePitch => shouldRandomizePitch;
    [SerializeField] bool shouldRandomizePitch = false;
    public float PitchVariation => pitchVariation;
    [SerializeField, Range(0, 3)] float pitchVariation = 0;

    public bool Loop => loop;
    [SerializeField] bool loop = false;

    [field: SerializeField] public Settings3D Settings3D { private set; get; }
}

[Serializable]
public class Settings3D
{
    public bool SpatialBlend => spatialBlend;
    [SerializeField] bool spatialBlend = false;

    public float MinDistance => minDistance;
    [SerializeField] float minDistance = 1;

    public float MaxDistance => maxDistance;
    [SerializeField] float maxDistance = 500;
}