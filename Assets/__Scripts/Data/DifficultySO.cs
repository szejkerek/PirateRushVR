// Copyright (c) Bartłomiej Gordon 2023. All rights reserved.

using UnityEngine;

/// <summary>
/// ScriptableObject representing difficulty settings for the game.
/// </summary>
[CreateAssetMenu(fileName = "SampleDifficultyLevel", menuName = "ScriptableObjects/Difficulty", order = 1)]
public class DifficultySO : ScriptableObject
{
    /// <summary>
    /// The name of the difficulty level.
    /// </summary>
    [field: SerializeField] public string DifficultyName { private set; get; }

    /// <summary>
    /// The tolerance range for a perfect slice.
    /// </summary>
    [field: SerializeField, Range(0f, 0.5f)] public float PerfectSliceTolerance { private set; get; }

    /// <summary>
    /// The chance of achieving a perfect shot.
    /// </summary>
    [field: SerializeField, Range(0f, 1f)] public float PerfectShootChance { private set; get; }

    /// <summary>
    /// The count of towers in the game.
    /// </summary>
    [field: SerializeField] public int TowerCount { private set; get; }

    /// <summary>
    /// The maximum health value.
    /// </summary>
    [field: SerializeField] public int MaxHealth { private set; get; }

    /// <summary>
    /// The ratio of bombs to neutral projectiles.
    /// </summary>
    [field: SerializeField, Range(0f, 1f)] public float BombToNeutralRatio { private set; get; }

    /// <summary>
    /// The chance of a special override occurrence.
    /// </summary>
    [field: SerializeField, Range(0f, 1f)] public float SpecialOverrideChance { private set; get; }

    /// <summary>
    /// The chance of a global combo happening.
    /// </summary>
    [field: SerializeField, Range(0f, 1f)] public float GlobalComboChance { private set; get; }

    /// <summary>
    /// The increment value for the multiplier.
    /// </summary>
    [field: SerializeField] public float MultiplierIncrement { private set; get; }

    /// <summary>
    /// Determines whether points decrease upon missing.
    /// </summary>
    [field: SerializeField] public bool DecrementPointsOnMiss { private set; get; }

    /// <summary>
    /// Determines whether the multiplier decreases upon missing.
    /// </summary>
    [field: SerializeField] public bool DecrementMultiplierOnMiss { private set; get; }

    /// <summary>
    /// The interval for the count of 25ms waits.
    /// </summary>
    [field: SerializeField] public Interval<int> CountOf25msWaits { private set; get; }
}
