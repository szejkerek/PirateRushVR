using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    protected override bool DidHit(out RaycastHit hit, int projectileLayer)
    {
        throw new System.NotImplementedException();
    }

    protected override void ShootableBehaviour(Projectile projectile)
    {
        throw new System.NotImplementedException();
    }

    protected override void SliceableBehavioir(Projectile projectile)
    {
        throw new System.NotImplementedException();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
