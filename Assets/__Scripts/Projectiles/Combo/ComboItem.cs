using UnityEngine;

/// <summary>
/// Represents a combo item with specific projectile and wait time.
/// </summary>
[System.Serializable]
public class ComboItem
{
    /// <summary>
    /// The type of projectile for this combo item.
    /// </summary>
    [field: SerializeField] public ComboSpawnType Projectile { private set; get; }

    /// <summary>
    /// The wait time associated with this combo item.
    /// </summary>
    [field: SerializeField] public ComboWaitTime Wait { private set; get; }
}
