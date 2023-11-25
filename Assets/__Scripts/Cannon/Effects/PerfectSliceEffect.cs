using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PerfectSlice", menuName = "Cannon/Projectile/Effects/PerfectSlice", order = 1)]
public class PerfectSlice : Effect
{
    public override void ApplyHitEffect(Projectile context)
    {
        AudioManager.Instance.PlayGlobal(AudioManager.Instance.SFXLib.PerfectSlice);
        Debug.Log("Perfect SliceableBehavioir");
    }
}
