using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboController : MonoBehaviour
{
    public int TickRate => _tickRate;
    int _tickRate = 0;

    int waitTicks = 0;
    ComboItemFactory comboItemFactory;

    Queue<ICannonBehaviour> queuedBehaviours = new Queue<ICannonBehaviour>();
    DifficultyLevel currentDifficulty;

    public CannonShooting Luncher => luncher;
    CannonShooting luncher;

    private void Start()
    {
        _tickRate = Systems.Instance.TickRate;
        comboItemFactory = new ComboItemFactory(this);
        luncher = GetComponent<CannonShooting>();
    }

    public void SetDifficulty(DifficultyLevel difficultyLevel)
    {
        currentDifficulty = difficultyLevel;
    }

    public void UpdateOnTick()
    {
        if (isPaused())
            return;

        if(queuedBehaviours.Count == 0)
        {
            queuedBehaviours.Enqueue(comboItemFactory.CreateSpawn(ComboItemType.Bomb));
            queuedBehaviours.Enqueue(comboItemFactory.CreateWait(ComboItemType.Interval25ms));
            queuedBehaviours.Enqueue(comboItemFactory.CreateWait(ComboItemType.Interval25ms));
            queuedBehaviours.Enqueue(comboItemFactory.CreateSpawn(ComboItemType.SpecialItem));
            queuedBehaviours.Enqueue(comboItemFactory.CreateWait(ComboItemType.Interval25ms));
            queuedBehaviours.Enqueue(comboItemFactory.CreateWait(ComboItemType.Interval25ms));
            queuedBehaviours.Enqueue(comboItemFactory.CreateSpawn(ComboItemType.NeutralProjectile));
            queuedBehaviours.Enqueue(comboItemFactory.CreateWait(ComboItemType.Interval25ms));
        }
        else
        {
            queuedBehaviours.Dequeue().Execute();
        }
    }

    private bool isPaused()
    {
        waitTicks--;
        waitTicks = Mathf.Max(0, waitTicks);
        return waitTicks > 0;
    }

    public void Wait(int waitTick)
    {
        this.waitTicks = waitTick;
    }
}
