// Copyright (c) Bartłomiej Gordon 2023. All rights reserved.
using System;
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    public static Action OnTick;
    public int TickRate => tickRate;
    [SerializeField] private int tickRate = 128;

    private float tickTimer = 0;
    
    void Awake()
    {
        OnTick += DebugMsg;
    }

    void Update()
    {
        UpdateTicks();
    }

    private void UpdateTicks()
    {
        if (tickTimer <= 0)
        {
            tickTimer = 1 / tickRate;
            OnTick?.Invoke();
        }
        tickTimer -= Time.deltaTime;
    }

    private void DebugMsg()
    {
        Debug.Log("Tick");
    }
}
