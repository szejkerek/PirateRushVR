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
            case ComboItemType.Interval25ms:
                break;
            case ComboItemType.Interval50ms:
                break;
            case ComboItemType.Interval75ms:
                break;
            case ComboItemType.Interval100ms:
                break;
            case ComboItemType.Interval150ms:
                break;
            default:
                break;
        }

        return new CannonWaitBehaviour(context, 50);
    }

    public ICannonBehaviour CreateSpawn(ComboItemType spawn)
    {
        switch (spawn)
        {
            case ComboItemType.NeutralProjectile:
                break;
            case ComboItemType.Bomb:
                break;
            case ComboItemType.SpecialItem:
                break;
            default:
                break;
        }
        return new CannonSpawnBehaviour(context, context.Luncher.BadBullet);
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