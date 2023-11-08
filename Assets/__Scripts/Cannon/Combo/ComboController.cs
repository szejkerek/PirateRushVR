
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
        currentDifficulty = Systems.Instance.difficultyLevel;
        _tickRate = Systems.Instance.TickRate;
        comboItemFactory = new ComboItemFactory(this);
        luncher = GetComponent<CannonShooting>();
    }

    public void UpdateOnTick()
    {
        if (isPaused())
            return;

        if(queuedBehaviours.Count != 0)
        {
            queuedBehaviours.Dequeue().Execute();
        }
        else
        {
            float comboChance = Random.Range(0f, 1f);
            if (comboChance <= currentDifficulty.GlobalComboChance)
            {
                AddGlobalCombo();
            }
            else
            {
                AddLocalCombo();
            }
        }
    }

    private void AddGlobalCombo()
    {
        ComboDatabase comboDatabase = CannonsManager.Instance.ComboDatabases.SelectRandomElement();
        foreach(var combo in comboDatabase.combos)
        {
            queuedBehaviours.Enqueue(comboItemFactory.CreateSpawn(combo.Type));
        }

    }

    private void AddLocalCombo()
    {
        ComboItemType itemType;
        Vector2Int wait = currentDifficulty.MinMaxCountOf25msIntervals;

        float specialValue = Random.Range(0f, 1f);
        if (specialValue <= currentDifficulty.SpecialOverrideChance)
        {
            itemType = ComboItemType.SpecialItem;
        }
        else
        {
            float randomValue = Random.Range(0f, 1f);
            itemType = (randomValue <= currentDifficulty.BombToNeutralRatio) ? ComboItemType.Bomb : ComboItemType.NeutralProjectile;
        }
        queuedBehaviours.Enqueue(comboItemFactory.CreateSpawn(itemType));


        int waitCount = Random.Range(wait.x, wait.y);
        for (int i = 0; i < waitCount; i++)
        {
            queuedBehaviours.Enqueue(comboItemFactory.CreateWait(ComboItemType.Interval25ms));
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
