using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CannonDatabase", menuName = "Cannon/Combo Database")]
public class ComboDatabase : ScriptableObject
{
    public List<ComboItem> combos = new List<ComboItem>();
}