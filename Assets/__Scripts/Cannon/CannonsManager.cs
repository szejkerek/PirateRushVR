// Copyright (c) Bartłomiej Gordon 2023. All rights reserved.
using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CannonsManager : Singleton<CannonsManager>
{
    [SerializeField] AssetLabelReference combosLabel;

    public List<ComboDatabase> ComboDatabases => comboDatabases;
    List<ComboDatabase> comboDatabases;

    List<ComboController> cannonsOnScene;
    bool isPaused = false;
    TickEngine tickEngine;

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

    void Update()
    {
        if (isPaused)
            return;

        tickEngine.UpdateTicks(Time.deltaTime);
    }

    public void Pause()
    {
        isPaused = true;
    }

    public void UnPause()
    {
        isPaused = false;
    }

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
