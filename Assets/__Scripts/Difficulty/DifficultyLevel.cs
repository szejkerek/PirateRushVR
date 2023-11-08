// Copyright (c) Bartłomiej Gordon 2023. All rights reserved.
using UnityEngine;

[CreateAssetMenu(fileName = "SampleDifficultyLevel", menuName = "ScriptableObjects/DifficultyLevel", order = 1)]
public class DifficultyLevel : ScriptableObject
{
    [field: SerializeField] public int TowerCount { private set; get; }
    [field: SerializeField, Range(0f, 1f)] public float BombToNeutralRatio { private set; get; }
    [field: SerializeField, Range(0f, 1f)] public float SpecialOverrideChance { private set; get; }
    [field: SerializeField, Range(0f, 1f)] public float GlobalComboChance { private set; get; }
    [field: SerializeField] public Vector2Int MinMaxCountOf25msIntervals { private set; get; }

}
