using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ComboDatabase", menuName = "Combo/Combo Database")]
public class ComboDatabase : ScriptableObject
{
    public List<ComboItem> combos = new List<ComboItem>();
}