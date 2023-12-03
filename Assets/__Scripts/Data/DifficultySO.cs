// Copyright (c) Bartłomiej Gordon 2023. All rights reserved.
using UnityEngine;

[CreateAssetMenu(fileName = "SampleDifficultyLevel", menuName = "ScriptableObjects/Difficulty", order = 1)]
public class DifficultySO : ScriptableObject
{
    [field: SerializeField] public string DifficultyName { private set; get; }
    [field: SerializeField, Range(0f, 0.5f)] public float PerfectSliceTolerance { private set; get; }
    [field: SerializeField, Range(0f, 1f)] public float PerfectShootChance { private set; get; }
    [field: SerializeField] public int TowerCount { private set; get; }
    [field: SerializeField] public int MaxHealth { private set; get; }
    [field: SerializeField, Range(0f, 1f)] public float BombToNeutralRatio { private set; get; }
    [field: SerializeField, Range(0f, 1f)] public float SpecialOverrideChance { private set; get; }
    [field: SerializeField, Range(0f, 1f)] public float GlobalComboChance { private set; get; }
    [field: SerializeField] public float MultiplierIncrement { private set; get; }
    [field: SerializeField] public bool DecrementPointsOnMiss { private set; get; }
    [field: SerializeField] public bool DecrementMultiplierOnMiss { private set; get; }
    [field: SerializeField] public Interval<int> CountOf25msWaits { private set; get; }

}
