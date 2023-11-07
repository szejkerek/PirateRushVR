// Copyright (c) Bartłomiej Gordon 2023. All rights reserved.
using System;

public class TickEngine
{
    private float _tickTimer = 0;
    private int _tickRate;

    /// <summary>
    /// Gets the tick rate of the TickEngine.
    /// </summary>
    public int TickRate => _tickRate;

    /// <summary>
    /// Initializes a new instance of the TickEngine class with the specified tick rate.
    /// </summary>
    /// <param name="tickRate">The desired tick rate in ticks per second.</param>
    /// <exception cref="ArgumentException">Thrown if tickRate is less than or equal to 0.</exception>
    public TickEngine(int tickRate)
    {
        if (tickRate <= 0)
        {
            throw new ArgumentException("Tick rate must be greater than 0.");
        }

        _tickRate = tickRate;
        _tickTimer = 1.0f / tickRate;
    }

    /// <summary>
    /// Updates the TickEngine with the elapsed time since the last update.
    /// </summary>
    /// <param name="delta">The elapsed time in seconds since the last update.</param>
    public void UpdateTicks(float delta)
    {
        _tickTimer -= delta;
        if (_tickTimer <= 0)
        {
            _tickTimer += 1.0f / _tickRate;
            OnTick?.Invoke();
        }
    }

    /// <summary>
    /// An event that is triggered when a tick occurs.
    /// </summary>
    public event Action OnTick;
}
