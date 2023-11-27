using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Cannon/Projectile/Projectile", order = 1)]
public class ProjectileSO : ScriptableObject
{
    [field: SerializeField] public ComboSpawnType ComboSpawnType { private set; get; }
    [field: SerializeField] public ProjectileType ProjectileType { private set; get; }
    [field: SerializeField] public GameObject Model { private set; get; }
    [field: SerializeField] public Material CrossSectionMaterial { private set; get; }
    [field: SerializeField] public float Points { private set; get; }
    [field: SerializeField] public bool AlwaysNegativePoints { private set; get; }
    [field: SerializeField] public Effect CriticalEffect { private set; get; }
    [field: SerializeField] public List<Effect> MutualEffects { private set; get; }
}
