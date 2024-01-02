using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controls the combo behavior in the game.
/// </summary>
public class ComboController : MonoBehaviour
{
    /// <summary>
    /// Gets the tick rate of the combo controller.
    /// </summary>
    public int TickRate => _tickRate;
    int _tickRate = 0;

    int waitTicks = 0;
    ComboItemFactory comboItemFactory;

    Queue<ICannonBehavior> queuedBehaviors = new Queue<ICannonBehavior>();
    DifficultySO currentDifficulty;

    /// <summary>
    /// Gets the cannon shooting component used for launching projectiles.
    /// </summary>
    public CannonShooting Launcher => launcher;
    CannonShooting launcher;

    private void Start()
    {
        currentDifficulty = Systems.Instance.difficultyLevel;
        _tickRate = Systems.Instance.TickRate;
        comboItemFactory = new ComboItemFactory(this);
        launcher = GetComponent<CannonShooting>();
        EnqueueRandomWaits(currentDifficulty.CountOf25msWaits);
    }

    /// <summary>
    /// Updates the combo controller on each tick.
    /// </summary>
    public void UpdateOnTick()
    {
        if (isPaused())
            return;

        if (queuedBehaviors.Count != 0)
        {
            queuedBehaviors.Dequeue().Execute();
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
        foreach (var combo in comboDatabase.combos)
        {
            queuedBehaviors.Enqueue(comboItemFactory.CreateSpawn(combo.Projectile));
            queuedBehaviors.Enqueue(comboItemFactory.CreateWait(combo.Wait));
        }

    }

    private void AddLocalCombo()
    {
        EnqueueRandomProjectile();
        EnqueueRandomWaits(currentDifficulty.CountOf25msWaits);
    }

    private void EnqueueRandomWaits(Interval<int> wait)
    {
        int waitCount = wait.GetValueBetween();
        for (int i = 0; i < waitCount; i++)
        {
            queuedBehaviors.Enqueue(comboItemFactory.CreateWait(ComboWaitTime.Interval25ms));
        }
    }

    private void EnqueueRandomProjectile()
    {
        ComboSpawnType itemType;
        float specialValue = Random.Range(0f, 1f);
        if (specialValue <= currentDifficulty.SpecialOverrideChance)
        {
            itemType = ComboSpawnType.SpecialItem;
        }
        else
        {
            float randomValue = Random.Range(0f, 1f);
            itemType = (randomValue <= currentDifficulty.BombToNeutralRatio) ? ComboSpawnType.Bomb : ComboSpawnType.NeutralProjectile;
        }
        queuedBehaviors.Enqueue(comboItemFactory.CreateSpawn(itemType));
    }

    private bool isPaused()
    {
        waitTicks--;
        waitTicks = Mathf.Max(0, waitTicks);
        return waitTicks > 0;
    }

    /// <summary>
    /// Sets the wait time in ticks.
    /// </summary>
    /// <param name="waitTick">The number of ticks to wait.</param>
    public void Wait(int waitTick)
    {
        this.waitTicks = waitTick;
    }
}
