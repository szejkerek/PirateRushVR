/// <summary>
/// Class defining a behavior where a cannon waits for a specific number of ticks.
/// Implements the ICannonBehavior interface.
/// </summary>
public class CannonWaitBehavior : ICannonBehavior
{
    // The number of ticks to wait
    int waitTickCount = 0;

    // The context of the combo controller
    ComboController context;

    /// <summary>
    /// Constructor for CannonWaitBehavior.
    /// </summary>
    /// <param name="cannonManager">The ComboController instance.</param>
    /// <param name="ticks">The number of ticks to wait.</param>
    public CannonWaitBehavior(ComboController cannonManager, int ticks)
    {
        waitTickCount = ticks;
        context = cannonManager;
    }

    /// <summary>
    /// Executes the behavior, causing the cannon to wait for the specified ticks.
    /// </summary>
    public void Execute()
    {
        context.Wait(waitTickCount);
    }
}
