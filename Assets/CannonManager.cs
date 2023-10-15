// Copyright (c) Bartłomiej Gordon 2023. All rights reserved.
using UnityEngine;

public class CannonManager : MonoBehaviour
{
    public int count = 0;
    [SerializeField] private int _tickRate = 32;
    private TickEngine tickEngine;

    void Awake()
    {
        tickEngine = new TickEngine(_tickRate);
        tickEngine.OnTick += DebugMsg;
    }

    void Update()
    {
        tickEngine.UpdateTicks(Time.deltaTime);
    }

    private void DebugMsg()
    {
        Debug.Log("Tick");
        count++;
    }
}
