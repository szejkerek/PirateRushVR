using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CannonDatabase", menuName = "Cannon/Combo Database")]
public class ComboDatabase : ScriptableObject
{
    [field: SerializeField] public List<ComboItem> combos { private set; get; }
}