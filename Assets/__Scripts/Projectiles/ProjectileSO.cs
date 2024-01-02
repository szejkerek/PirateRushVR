using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject representing a projectile in the cannon.
/// </summary>
[CreateAssetMenu(fileName = "Projectile", menuName = "Cannon/Projectile/Projectile", order = 1)]
public class ProjectileSO : ScriptableObject
{
    /// <summary>
    /// The type of combo spawn associated with the projectile.
    /// </summary>
    [field: SerializeField] public ComboSpawnType ComboSpawnType { private set; get; }

    /// <summary>
    /// The type of the projectile.
    /// </summary>
    [field: SerializeField] public ProjectileType ProjectileType { private set; get; }

    /// <summary>
    /// The visual model of the projectile.
    /// </summary>
    [field: SerializeField] public GameObject Model { private set; get; }

    /// <summary>
    /// The points earned by hitting the target with this projectile.
    /// </summary>
    [field: SerializeField] public float Points { private set; get; }

    /// <summary>
    /// List of mutual effects applied when the projectile hits a target.
    /// </summary>
    [field: SerializeField] public List<Effect> MutualEffects { private set; get; }

    /// <summary>
    /// Optional data associated with the projectile.
    /// </summary>
    [field: SerializeField] public ProjectileOptionalData OptionalData { private set; get; }
}
