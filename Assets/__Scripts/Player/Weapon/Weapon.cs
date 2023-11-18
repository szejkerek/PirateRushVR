using System;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] LayerMask mask;
    private void Update()
    {
        if (DidHit(out RaycastHit hit, mask))
        {
            if (hit.transform.gameObject.TryGetComponent(out Projectile projectile))
            {
                switch (projectile.Data.Type)
                {
                    case ComboSpawnType.NeutralProjectile:
                        SliceableBehavioir(projectile);
                        break;
                    case ComboSpawnType.Bomb:
                        CollectibleBehaviour(projectile);
                        break;
                    case ComboSpawnType.SpecialItem:
                        ShootableBehaviour(projectile);
                        break;
                }

            }
        }
    }

    protected abstract bool DidHit(out RaycastHit hit, int projectileLayer);
    protected abstract void ShootableBehaviour(Projectile projectile);
    protected abstract void SliceableBehavioir(Projectile projectile);
    private void CollectibleBehaviour(Projectile projectile)
    {
        projectile.ApplyEffects(false);
        Debug.Log("Hit collectible layer");
        Destroy(gameObject);     
    }
    
}