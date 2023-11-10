using UnityEngine;

[System.Serializable]
public class ComboItem
{
    [field: SerializeField] public ComboSpawnType Projectile { private set; get; }
    public ComboWaitTime Wait => wait;
    [SerializeField] private ComboWaitTime wait = ComboWaitTime.Interval50ms;
}
