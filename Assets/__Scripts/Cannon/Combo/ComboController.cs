using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboController : MonoBehaviour
{
    int waitTick = 0;
    ComboItemFactory comboItemFactory;

    Queue<ICannonBehaviour> queuedBehaviours = new Queue<ICannonBehaviour>();
    DifficultyLevel currentDifficulty;

    public CannonShooting Luncher => luncher;
    CannonShooting luncher;

    private void Awake()
    {
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
            queuedBehaviours.Enqueue(comboItemFactory.CreateWait(ComboItemType.Interval50ms));
        }
        else
        {
            queuedBehaviours.Dequeue().Execute();
        }
    }

    private bool isPaused()
    {
        waitTick--;
        waitTick = Mathf.Max(0, waitTick);
        return waitTick > 0;
    }

    public void Wait(int waitTick)
    {
        this.waitTick = waitTick;
    }
}
