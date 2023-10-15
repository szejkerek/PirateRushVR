// Copyright (c) Bartłomiej Gordon 2023. All rights reserved.

using System;

public class TickEngine
{
    public Action OnTick;
    public int TickRate => _tickRate;
    private int _tickRate;

    public TickEngine(int tickRate)
    {
        _tickRate = tickRate;
    }

    private float tickTimer = 0;
    public void UpdateTicks(float delta)
    {
        tickTimer -= delta;
        if (tickTimer <= 0)
        {
            tickTimer = 1 / Convert.ToSingle(_tickRate);
            OnTick?.Invoke();
        }
    }
}
