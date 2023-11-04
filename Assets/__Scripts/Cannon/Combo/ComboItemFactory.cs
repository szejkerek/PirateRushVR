using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboItemFactory
{
    ComboManager context;
    public ComboItemFactory(ComboManager comboManager)
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
        return new CannonSpawnBehaviour(context, GameObject.CreatePrimitive(PrimitiveType.Cube));
    }

}


public interface ICannonBehaviour
{
    public void Execute();
}

public class CannonWaitBehaviour : ICannonBehaviour
{
    int waitTickCount = 0;
    ComboManager context;

    public CannonWaitBehaviour(ComboManager cannonManager, int ticks)
    {
        waitTickCount = ticks;
        context = cannonManager;
    }

    public void Execute()
    {
        Debug.Log($"Wait for {waitTickCount}");
    }
}

public class CannonSpawnBehaviour : ICannonBehaviour
{
    GameObject projectile;
    ComboManager context;

    public CannonSpawnBehaviour(ComboManager cannonManager, GameObject projectile)
    {
        this.projectile = projectile;
        context = cannonManager;
    }

    public void Execute()
    {
        Debug.Log($"Spawning {projectile.name}");
    }
}