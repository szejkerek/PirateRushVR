using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// Manages the Cannons in the game.
/// </summary>
public class CannonsManager : Singleton<CannonsManager>
{
    [SerializeField] AssetLabelReference combosLabel;

    /// <summary>
    /// List of Combo Databases.
    /// </summary>
    public List<ComboDatabase> ComboDatabases => comboDatabases;
    List<ComboDatabase> comboDatabases;

    List<ComboController> cannonsOnScene;
    bool isPaused = false;
    TickEngine tickEngine;

    /// <summary>
    /// Initializes the tick engine and spawns cannons.
    /// </summary>
    void Start()
    {
        tickEngine = new TickEngine(Systems.Instance.TickRate);

        comboDatabases = DataLoader<ComboDatabase>.Load(combosLabel);

        int towerCount = Systems.Instance.difficultyLevel.TowerCount;
        SpawnCannons(towerCount);
        cannonsOnScene.ForEach(cannon =>
        {
            tickEngine.OnTick += cannon.UpdateOnTick;
        });
    }

    /// <summary>
    /// Updates the tick engine.
    /// </summary>
    void Update()
    {
        if (isPaused)
            return;

        tickEngine.UpdateTicks(Time.deltaTime);
    }

    /// <summary>
    /// Pauses the Cannons.
    /// </summary>
    public void Pause()
    {
        isPaused = true;
    }

    /// <summary>
    /// Unpauses the Cannons.
    /// </summary>
    public void UnPause()
    {
        isPaused = false;
    }

    /// <summary>
    /// Clears all projectiles from the scene.
    /// </summary>
    public void ClearAllProjectiles()
    {
        Projectile[] allProjectiles = FindObjectsOfType<Projectile>();
        foreach (Projectile projectile in allProjectiles)
        {
            if (projectile != null && projectile.gameObject != null)
            {
                projectile.gameObject.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Spawns cannons based on the count.
    /// </summary>
    void SpawnCannons(int count)
    {
        cannonsOnScene = GetComponentsInChildren<ComboController>().ToList();

        int deactivateCount = 0;
        if (count <= cannonsOnScene.Count)
        {
            deactivateCount = cannonsOnScene.Count - count;
        }

        List<ComboController> cannonsToBeRemoved = cannonsOnScene.SelectRandomElements(deactivateCount);
        cannonsToBeRemoved.ForEach((cannon) =>
        {
            cannon.gameObject.SetActive(false);
            cannonsOnScene.Remove(cannon);
        });
    }
}
