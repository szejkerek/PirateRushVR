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
            case ComboItemType.NeutralProjectile:  return new CannonSpawnBehaviour(context, context.Luncher.GoodBullet);
            case ComboItemType.Bomb:               return new CannonSpawnBehaviour(context, context.Luncher.BadBullet);
            case ComboItemType.SpecialItem:        return new CannonSpawnBehaviour(context, context.Luncher.SpecialBullet);
            default:                               return new CannonSpawnBehaviour(context, context.Luncher.GoodBullet);
        }       
    }
}


public interface ICannonBehaviour
{
    public void Execute();
}

public class CannonWaitBehaviour : ICannonBehaviour
{
    int waitTickCount = 0;
    ComboController context;

    public CannonWaitBehaviour(ComboController cannonManager, int ticks)
    {
        waitTickCount = ticks;
        context = cannonManager;
    }

    public void Execute()
    {
        context.Wait(waitTickCount);
    }
}

public class CannonSpawnBehaviour : ICannonBehaviour
{
    Projectile projectile;
    ComboController context;

    public CannonSpawnBehaviour(ComboController cannonManager, Projectile projectile)
    {
        this.projectile = projectile;
        context = cannonManager;
    }

    public void Execute()
    {
        context.Luncher.Shoot(projectile);
    }
}