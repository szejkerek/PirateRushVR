// Copyright (c) Bartłomiej Gordon 2023. All rights reserved.
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CannonsManager : Singleton<CannonsManager>
{
    [SerializeField] AssetLabelReference combosLabel;

    public List<ComboDatabase> ComboDatabases => comboDatabases;
    List<ComboDatabase> comboDatabases;


    TickEngine tickEngine;
    List<ComboController> cannonsOnScene;

    void Start()
    {        
        tickEngine = new TickEngine(Systems.Instance.TickRate);      
       
        DataLoader<ComboDatabase> dataLoader = new DataLoader<ComboDatabase>();
        comboDatabases = dataLoader.Load(combosLabel);

        int towerCount = Systems.Instance.difficultyLevel.TowerCount;
        SpawnCannons(towerCount);
        cannonsOnScene.ForEach(cannon =>
        {
            tickEngine.OnTick += cannon.UpdateOnTick;
        });
    }

    void Update()
    {
        tickEngine.UpdateTicks(Time.deltaTime);
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
