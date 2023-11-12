// Copyright (c) Bartłomiej Gordon 2023. All rights reserved.
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CannonsManager : Singleton<CannonsManager>
{
    [SerializeField] AssetLabelReference combosLabel;

    private TickEngine tickEngine;
    List<Cannon> cannonsOnScene;

    public List<ComboDatabase> ComboDatabases => comboDatabases;
    List<ComboDatabase> comboDatabases;    
    
    protected override void Awake()
    {        
        base.Awake();
        tickEngine = new TickEngine(Systems.Instance.TickRate);      
       
        DataLoader<ComboDatabase> dataLoader = new DataLoader<ComboDatabase>();
        comboDatabases = dataLoader.Load(combosLabel);

        int towerCount = FindObjectOfType<Systems>().difficultyLevel.TowerCount;
        SpawnCannons(towerCount);
        cannonsOnScene.ForEach(cannon =>
        {
            tickEngine.OnTick += cannon.ComboManager.UpdateOnTick;
        });
    }

    private void Start()
    {

    }

    void Update()
    {
        tickEngine.UpdateTicks(Time.deltaTime);
    }

    void SpawnCannons(int count)
    {
        cannonsOnScene = GetComponentsInChildren<Cannon>().ToList();

        int deactivateCount = 0;
        if (count <= cannonsOnScene.Count)
        {
            deactivateCount = cannonsOnScene.Count - count;
        }

        List<Cannon> cannonsToBeRemoved = cannonsOnScene.SelectRandomElements(deactivateCount);
        cannonsToBeRemoved.ForEach((cannon) =>
        {
            cannon.gameObject.SetActive(false);
            cannonsOnScene.Remove(cannon);
        });
    }

}
