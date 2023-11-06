// Copyright (c) Bartłomiej Gordon 2023. All rights reserved.
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CannonsManager : Singleton<CannonsManager>
{   
    private TickEngine tickEngine;
    List<Cannon> cannonsOnScene;

    void Start()
    {
        tickEngine = new TickEngine(Systems.Instance.TickRate);       
        SpawnCannons(Systems.Instance.difficultyLevel.TowerCount);
        cannonsOnScene.ForEach(cannon => tickEngine.OnTick += cannon.ComboManager.UpdateOnTick);
    }

    void Update()
    {
        tickEngine.UpdateTicks(Time.deltaTime);
    }

    void SpawnCannons(int count)
    {
        cannonsOnScene = GetComponentsInChildren<Cannon>().ToList();
        int deactivateCount = cannonsOnScene.Count - count;

        List<Cannon> cannonsToBeRemoved = cannonsOnScene.SelectRandomElements(deactivateCount);
        cannonsToBeRemoved.ForEach((cannon) =>
        {
            cannon.Deactivate();
            cannonsOnScene.Remove(cannon);
        });
    }

}
