/// <summary>
/// Class defining a behavior where a cannon spawns a projectile.
/// Implements the ICannonBehavior interface.
/// </summary>
public class CannonSpawnBehavior : ICannonBehavior
{
    // The projectile to be spawned
    ProjectileSO projectile;

    // The context of the combo controller
    ComboController context;

    /// <summary>
    /// Constructor for CannonSpawnBehavior.
    /// </summary>
    /// <param name="cannonManager">The ComboController instance.</param>
    /// <param name="projectile">The ProjectileSO instance for spawning.</param>
    public CannonSpawnBehavior(ComboController cannonManager, ProjectileSO projectile)
    {
        this.projectile = projectile;
        context = cannonManager;
    }

    /// <summary>
    /// Executes the behavior, making the cannon shoot the projectile.
    /// </summary>
    public void Execute()
    {
        context.Launcher.Shoot(projectile);
    }
}
