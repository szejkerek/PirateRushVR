using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    int waitTick = 0;
    ComboItemFactory comboItemFactory;

    Queue<ICannonBehaviour> queuedBehaviours = new Queue<ICannonBehaviour>();
    DifficultyLevel currentDifficulty;

    private void Awake()
    {
        comboItemFactory = new ComboItemFactory(this);
    }

    public void SetDifficulty(DifficultyLevel _difficultyLevel)
    {
        currentDifficulty = _difficultyLevel;
    }

    public void UpdateOnTick()
    {
        if (isPaused())
            return;
    }

    private bool isPaused()
    {
        waitTick--;
        waitTick = Mathf.Max(0, waitTick);
        return waitTick > 0;
    }


}
