public class CannonWaitBehaviour : ICannonBehaviour
{
    int waitTickCount = 0;
    ComboController context;

    public CannonWaitBehaviour(ComboController cannonManager, int ticks)
    {
        waitTickCount = ticks;
        context = cannonManager;
    }

    public void Execute()
    {
        context.Wait(waitTickCount);
    }
}
