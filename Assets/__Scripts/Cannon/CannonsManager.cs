// Copyright (c) Bartłomiej Gordon 2023. All rights reserved.
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CannonsManager : MonoBehaviour
{
    public int TickRate => _tickRate;
    [SerializeField] private int _tickRate = 32;
    
    private TickEngine tickEngine;
    List<Cannon> cannonsOnScene;

    DifficultyLevel _difficultyLevel;

    void Start()
    {
        _difficultyLevel = Systems.Instance.difficultyLevel;
        tickEngine = new TickEngine(_tickRate);       
        SpawnCannons(_difficultyLevel.TowerCount);
        cannonsOnScene.ForEach(cannon => {
            cannon.ComboManager.SetDifficulty(_difficultyLevel);
            tickEngine.OnTick += cannon.ComboManager.UpdateOnTick;
        }      
        );
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
