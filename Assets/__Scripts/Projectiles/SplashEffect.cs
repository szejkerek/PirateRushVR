using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages the splash effect using two particle systems.
/// </summary>
public class SplashEffect : MonoBehaviour
{
    [SerializeField] ParticleSystem ps_big;
    [SerializeField] ParticleSystem ps_small;

    /// <summary>
    /// Initializes the splash effect with a specified main color.
    /// </summary>
    /// <param name="mainColor">The main color for the splash effect.</param>
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
