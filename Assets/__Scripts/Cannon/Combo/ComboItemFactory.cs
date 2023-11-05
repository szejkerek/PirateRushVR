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

    public ICannonBehaviour CreateWait(ComboItemType wait)
    {
        switch (wait)
        {
            case ComboItemType.Interval25ms:        return new CannonWaitBehaviour(context, Mathf.CeilToInt(context.TickRate * 0.25f));
            case ComboItemType.Interval50ms:        return new CannonWaitBehaviour(context, Mathf.CeilToInt(context.TickRate * 0.50f));
            case ComboItemType.Interval75ms:        return new CannonWaitBehaviour(context, Mathf.CeilToInt(context.TickRate * 0.75f));
            case ComboItemType.Interval100ms:       return new CannonWaitBehaviour(context, Mathf.CeilToInt(context.TickRate * 1));
            case ComboItemType.Interval150ms:       return new CannonWaitBehaviour(context, Mathf.CeilToInt(context.TickRate * 1.5f));
            default:                                return new CannonWaitBehaviour(context, Mathf.CeilToInt(context.TickRate * 0.50f));    

        }     
    }

    public ICannonBehaviour CreateSpawn(ComboItemType spawn)
    {
        switch (spawn)
        {
            case ComboItemType.NeutralProjectile:  return new CannonSpawnBehaviour(context, context.Luncher.GoodBullets.SelectRandomElement());
            case ComboItemType.Bomb:               return new CannonSpawnBehaviour(context, context.Luncher.BadBullets.SelectRandomElement());
            case ComboItemType.SpecialItem:        return new CannonSpawnBehaviour(context, context.Luncher.SpecialBullets.SelectRandomElement());
            default:                               return new CannonSpawnBehaviour(context, context.Luncher.GoodBullets.SelectRandomElement());
        }       
    }
}
