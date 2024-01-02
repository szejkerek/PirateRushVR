using System;
using UnityEngine;

/// <summary>
/// Manages global game systems and settings.
/// </summary>
public class Systems : Singleton<Systems>
{
    /// <summary>
    /// Indicates if the katana is held in the right hand.
    /// </summary>
    public bool KatanaRight = false;

    /// <summary>
    /// Determines the tick rate for game updates.
    /// </summary>
    public int TickRate = 32;

    /// <summary>
    /// Stores the current difficulty level settings.
    /// </summary>
    public DifficultySO difficultyLevel;
}
