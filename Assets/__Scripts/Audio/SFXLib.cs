using UnityEngine;

[CreateAssetMenu(fileName = "SFXLib", menuName = "Audio/Libraries/SFXLib", order = 1)]
public class SFXLib : ScriptableObject
{
    [field: SerializeField] public Sound PerfectSlice { private set; get; }
}