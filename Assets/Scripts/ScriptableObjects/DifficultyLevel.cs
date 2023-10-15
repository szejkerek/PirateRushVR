// Copyright (c) Bartłomiej Gordon 2023. All rights reserved.
using UnityEngine;

[CreateAssetMenu(fileName = "SampleDifficultyLevel", menuName = "ScriptableObjects/DifficultyLevel", order = 1)]
public class DifficultyLevel : ScriptableObject
{
    [field: SerializeField] public int TowerCount { private set; get; }
}
