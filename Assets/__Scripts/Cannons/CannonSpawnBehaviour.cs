public class CannonSpawnBehavior : ICannonBehavior
{
    ProjectileSO projectile;
    ComboController context;

    public CannonSpawnBehavior(ComboController cannonManager, ProjectileSO projectile)
    {
        this.projectile = projectile;
        context = cannonManager;
    }

    public void Execute()
    {
        context.Launcher.Shoot(projectile);
    }
}