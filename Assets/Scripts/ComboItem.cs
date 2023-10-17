using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public enum ComboItemType
{
    NeutralProjectile,
    Bomb,
    SpecialItem,
    Interval25ms,
    Interval50ms,
    Interval75ms,
    Interval100ms,
    Interval150ms,
}

[System.Serializable]
public class ComboItem
{
    [field: SerializeField] public ComboItemType Type { private set; get; }
}

[CreateAssetMenu(fileName = "ComboDatabase", menuName = "Combo/Combo Database")]
public class ComboDatabase : ScriptableObject
{
    public List<ComboItem> combos = new List<ComboItem>();
}