using UnityEngine;

[System.Serializable]
public class ComboItem
{
    [field: SerializeField] public ComboSpawnType Projectile { private set; get; }
    [field: SerializeField] public ComboWaitTime Wait { private set; get; }
}
