// Copyright (c) Bartłomiej Gordon 2023. All rights reserved.
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class CannonsManager : Singleton<CannonsManager>
{
    [SerializeField] string playerTag;
    [SerializeField] AssetLabelReference combosLabel;

    private TickEngine tickEngine;
    List<Cannon> cannonsOnScene;

    public List<ComboDatabase> ComboDatabases => comboDatabases;
    List<ComboDatabase> comboDatabases;    
    
    public CannonSettings CannonSettings => cannonSettings;
    [SerializeField] CannonSettings cannonSettings;


    void Start()
    {
        Transform target = GameObject.FindGameObjectWithTag(playerTag).transform;

        tickEngine = new TickEngine(Systems.Instance.TickRate);      
        
        DataLoader<ComboDatabase> dataLoader = new DataLoader<ComboDatabase>();
        comboDatabases = dataLoader.Load(combosLabel);

        SpawnCannons(Systems.Instance.difficultyLevel.TowerCount);
        cannonsOnScene.ForEach(cannon =>
        {
            tickEngine.OnTick += cannon.ComboManager.UpdateOnTick;
            cannon.Luncher.SetTarget(target);
        });
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
            cannon.Deactivate();
            cannonsOnScene.Remove(cannon);
        });
    }

}
