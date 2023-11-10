using System;
using System.Collections;
using System.Collections.Generic;
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
        ProjectileSO selectedBullet = context.Launcher.GoodBullets.SelectRandomElement();
        switch (spawn)
        {
            case ComboSpawnType.NeutralProjectile:
                selectedBullet = context.Launcher.GoodBullets.SelectRandomElement();
                break;
            case ComboSpawnType.Bomb:
                selectedBullet = context.Launcher.BadBullets.SelectRandomElement();
                break;
            case ComboSpawnType.SpecialItem:
                selectedBullet = context.Launcher.SpecialBullets.SelectRandomElement();
                break;
        }
        return new CannonSpawnBehavior(context, selectedBullet);
    }

    private int CalculateTicks(float seconds)
    {
        return Mathf.CeilToInt(context.TickRate * seconds);
    }
}
