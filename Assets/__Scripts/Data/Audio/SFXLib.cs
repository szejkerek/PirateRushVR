using UnityEngine;

/// <summary>
/// ScriptableObject representing a library for sound effects.
/// </summary>
[CreateAssetMenu(fileName = "SFXLib", menuName = "Audio/Libraries/SFXLib", order = 1)]
public class SFXLib : ScriptableObject
{
    /// <summary>
    /// The sound for critical strikes.
    /// </summary>
    [field: SerializeField] public Sound CriticalStrike { private set; get; }
}
