public class CannonWaitBehavior : ICannonBehavior
{
    int waitTickCount = 0;
    ComboController context;

    public CannonWaitBehavior(ComboController cannonManager, int ticks)
    {
        waitTickCount = ticks;
        context = cannonManager;
    }

    public void Execute()
    {
        context.Wait(waitTickCount);
    }
}
