public class CannonSpawnBehaviour : ICannonBehaviour
{
    ProjectileSO projectile;
    ComboController context;

    public CannonSpawnBehaviour(ComboController cannonManager, ProjectileSO projectile)
    {
        this.projectile = projectile;
        context = cannonManager;
    }

    public void Execute()
    {
        context.Luncher.Shoot(projectile);
    }
}