using System.Collections;
using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] Sound plumSound;

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
                    if(!projectile.PointsChanged)
                        ScoreManager.Instance.DecrementMultiplier();
                }

                Destroy(projectile.gameObject, 1f); // Start coroutine to destroy after 3 seconds
            }
        }

        AudioManager.Instance.PlayAtPosition(other.transform.position, plumSound);
    }
}
