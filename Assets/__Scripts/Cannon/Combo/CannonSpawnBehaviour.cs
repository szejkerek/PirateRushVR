public class CannonSpawnBehaviour : ICannonBehaviour
{
    Projectile projectile;
    ComboController context;

    public CannonSpawnBehaviour(ComboController cannonManager, Projectile projectile)
    {
        this.projectile = projectile;
        context = cannonManager;
    }

    public void Execute()
    {
        context.Luncher.Shoot(projectile);
    }
}