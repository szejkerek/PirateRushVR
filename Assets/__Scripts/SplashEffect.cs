using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem ps_big;
    [SerializeField] ParticleSystem ps_small;

    public void Init(Color mainColor)
    {
        var mainColorModuleBig = ps_big.main;
        mainColorModuleBig.startColor = mainColor;

        var mainColorModuleSmall = ps_small.main;
        Color smallParticleColor = mainColor; // Use the mainColor as a base
        smallParticleColor.a = 0.8f; // Set alpha to 0.8

        mainColorModuleSmall.startColor = smallParticleColor;
    }
}
