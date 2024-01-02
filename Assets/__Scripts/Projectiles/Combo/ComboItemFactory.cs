using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

/// <summary>
/// Factory class responsible for creating combo-related behaviors.
/// </summary>
public class ComboItemFactory
{
    ComboController context;

    /// <summary>
    /// Initializes a new instance of the ComboItemFactory with the provided ComboController.
    /// </summary>
    /// <param name="comboManager">The ComboController instance to use as context.</param>
    public ComboItemFactory(ComboController comboManager)
    {
        context = comboManager;
    }

    /// <summary>
    /// Creates a wait behavior based on the provided ComboWaitTime.
    /// </summary>
    /// <param name="wait">The type of wait time for the behavior.</param>
    /// <returns>An ICannonBehavior representing the wait behavior.</returns>
    public ICannonBehavior CreateWait(ComboWaitTime wait)
    {
        int ticks = CalculateTicks(0.50f); // Default ticks for 0.50s
        switch (wait)
        {
            case ComboWaitTime.Interval25ms:
                ticks = CalculateTicks(0.25f);
                break;
            case ComboWaitTime.Interval50ms:
                ticks = CalculateTicks(0.50f);
                break;
            case ComboWaitTime.Interval75ms:
                ticks = CalculateTicks(0.75f);
                break;
            case ComboWaitTime.Interval100ms:
                ticks = CalculateTicks(1);
                break;
            case ComboWaitTime.Interval150ms:
                ticks = CalculateTicks(1.5f);
                break;
        }
        return new CannonWaitBehavior(context, ticks);
    }

    /// <summary>
    /// Creates a spawn behavior based on the provided ComboSpawnType.
    /// </summary>
    /// <param name="spawn">The type of spawn for the behavior.</param>
    /// <returns>An ICannonBehavior representing the spawn behavior.</returns>
    public ICannonBehavior CreateSpawn(ComboSpawnType spawn)
    {
        ProjectileSO selectedBullet = SelectSubset(spawn).SelectRandomElement();
        return new CannonSpawnBehavior(context, selectedBullet);
    }

    /// <summary>
    /// Calculates ticks based on the provided seconds.
    /// </summary>
    /// <param name="seconds">The time in seconds to convert into ticks.</param>
    /// <returns>The calculated number of ticks.</returns>
    private int CalculateTicks(float seconds)
    {
        return Mathf.CeilToInt(context.TickRate * seconds);
    }

    /// <summary>
    /// Selects a subset of projectiles based on the ComboSpawnType.
    /// </summary>
    /// <param name="type">The ComboSpawnType to filter the projectiles.</param>
    /// <returns>A list of ProjectileSO representing the subset of projectiles.</returns>
    private List<ProjectileSO> SelectSubset(ComboSpawnType type)
    {
        List<ProjectileSO> allBullets = context.Launcher.Settings.Projectiles;

        List<ProjectileSO> typeSubset = allBullets
            .Where(projectile => projectile.ComboSpawnType == type)
            .ToList();

        if (typeSubset.Count == 0) // No items of this type
        {
            Debug.LogError($"No projectiles of type {type} in projectile list!");
            return null;
        }
        else
        {
            return typeSubset;
        }
    }
}
