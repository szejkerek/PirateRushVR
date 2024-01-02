using System;
using UnityEngine;

/// <summary>
/// ScriptableObject representing an audio clip with settings.
/// </summary>
[CreateAssetMenu(fileName = "Sound", menuName = "Audio/Sound", order = 1)]
public class Sound : ScriptableObject
{
    /// <summary>
    /// The audio clip for the sound.
    /// </summary>
    [field: SerializeField]
    public AudioClip Clip { private set; get; }

    /// <summary>
    /// The volume of the sound.
    /// </summary>
    public float Volume => volume;
    [SerializeField, Range(0, 1)] float volume = 1;

    /// <summary>
    /// The pitch variation of the sound.
    /// </summary>
    public float PitchVariation => pitchVariation;
    [SerializeField, Range(0, 3)] float pitchVariation = 0;

    /// <summary>
    /// Indicates whether the sound should loop.
    /// </summary>
    public bool Loop => loop;
    [SerializeField] bool loop = false;

    /// <summary>
    /// Settings for 3D sound.
    /// </summary>
    [field: SerializeField]
    public Settings3D Settings3D { private set; get; }
}

/// <summary>
/// Class containing settings for 3D sound.
/// </summary>
[Serializable]
public class Settings3D
{
    /// <summary>
    /// Determines the spatial blend for the sound.
    /// </summary>
    public bool SpatialBlend => spatialBlend;
    [SerializeField] bool spatialBlend = false;

    /// <summary>
    /// The minimum distance for the sound.
    /// </summary>
    public float MinDistance => minDistance;
    [SerializeField] float minDistance = 1;

    /// <summary>
    /// The maximum distance for the sound.
    /// </summary>
    public float MaxDistance => maxDistance;
    [SerializeField] float maxDistance = 500;
}
