using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject representing a database of combo items.
/// </summary>
[CreateAssetMenu(fileName = "CannonDatabase", menuName = "Cannon/Combo Database")]
public class ComboDatabase : ScriptableObject
{
    /// <summary>
    /// List of combo items in the database.
    /// </summary>
    [field: SerializeField] public List<ComboItem> combos { private set; get; }
}
