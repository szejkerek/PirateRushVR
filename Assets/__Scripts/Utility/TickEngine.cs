using System;

/// <summary>
/// Handles tick-based updates and invokes events at a specified tick rate.
/// </summary>
public class TickEngine
{
    /// <summary>
    /// Event invoked on each tick.
    /// </summary>
    public event Action OnTick;

    private float _tickTimer = 0;
    private int _tickRate;

    /// <summary>
    /// The tick rate at which ticks are processed.
    /// </summary>
    public int TickRate => _tickRate;

    /// <summary>
    /// Initializes a new instance of the TickEngine class with a specified tick rate.
    /// </summary>
    /// <param name="tickRate">The rate at which ticks will occur (ticks per second).</param>
    /// <exception cref="ArgumentException">Thrown when the tick rate is less than or equal to 0.</exception>
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
    /// Updates the ticks based on the elapsed time.
    /// </summary>
    /// <param name="delta">The time passed since the last update.</param>
    public void UpdateTicks(float delta)
    {
        _tickTimer -= delta;
        if (_tickTimer <= 0)
        {
            _tickTimer += 1.0f / _tickRate;
            OnTick?.Invoke();
        }
    }
}
