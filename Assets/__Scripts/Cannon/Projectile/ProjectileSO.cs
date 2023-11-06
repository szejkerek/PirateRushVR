using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Projectile", menuName = "Projectile/Projectile", order = 1)]
public class ProjectileSO : ScriptableObject
{
    [field: SerializeField] public string ProjectileName { private set; get; }
    [field: SerializeField] public GameObject Model { private set; get; }
    [field: SerializeField] public List<EffectSO> Effects { private set; get; }
}