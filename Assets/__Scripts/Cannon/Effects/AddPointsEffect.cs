using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AddPoints", menuName = "Cannon/Projectile/Effects/AddPoints", order = 1)]
public class AddPointsEffect : Effect
{
    [field: SerializeField] public float Points { private set; get; }

    public override void ApplyHitEffect(Projectile context)
    {
        if (Points <= 0)
            return;

        ScoreManager.Instance.AddPoints(Points);

        Debug.Log($"Added {Points} for {Systems.Instance.Nickname}!");
    }
}
