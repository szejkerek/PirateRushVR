using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComboItemFactory
{
    ComboController context;
    public ComboItemFactory(ComboController comboManager)
    {
        context = comboManager;
    }

    public ICannonBehavior CreateWait(ComboWaitTime wait)
    {
        int ticks = CalculateTicks(0.50f);
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

    public ICannonBehavior CreateSpawn(ComboSpawnType spawn)
    {
        ProjectileSO selectedBullet = SelectSubset(spawn).SelectRandomElement();
        return new CannonSpawnBehavior(context, selectedBullet);
    }

    private int CalculateTicks(float seconds)
    {
        return Mathf.CeilToInt(context.TickRate * seconds);
    }

    private List<ProjectileSO> SelectSubset(ComboSpawnType type)
    {
        List<ProjectileSO> allBullets = context.Launcher.Settings.Projectiles;

        List<ProjectileSO> typeSubset = allBullets
       .Where(projectile => projectile.Type == type)
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
