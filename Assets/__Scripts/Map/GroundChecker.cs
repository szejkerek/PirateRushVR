using System.Collections;
using UnityEngine;

/// <summary>
/// Checks for collisions with the ground and handles associated actions.
/// </summary>
public class GroundChecker : MonoBehaviour
{
    [SerializeField] Sound plumSound;

    /// <summary>
    /// Triggers actions upon collision with other objects.
    /// </summary>
    /// <param name="other">The collider that this object collided with.</param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Projectile projectile))
        {
            if (projectile.Data.ProjectileType != ProjectileType.Collectible)
            {
                if (Systems.Instance.difficultyLevel.DecrementPointsOnMiss)
                {
                    projectile.ApplyPoints(negative: true);
                }

                if (Systems.Instance.difficultyLevel.DecrementMultiplierOnMiss)
                {
                    if (!projectile.PointsChanged)
                        ScoreManager.Instance.DecrementMultiplier();
                }

                Destroy(projectile.gameObject, 1f); // Start coroutine to destroy after 3 seconds
            }
        }

        if (other.TryGetComponent(out Bomb bomb))
        {
            bomb.PutDownFire();
        }

        AudioManager.Instance.PlayAtPosition(other.transform.position, plumSound);
    }
}
