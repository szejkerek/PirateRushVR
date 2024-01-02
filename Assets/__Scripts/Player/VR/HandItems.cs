using System;
using UnityEngine;

/// <summary>
/// Manages different hand-held items and associated models.
/// </summary>
public class HandItems : MonoBehaviour
{
    public GameObject Model;
    public GameObject Katana;
    public GameObject KatanaHand;
    public GameObject Pistol;
    public GameObject PistolHand;
    public GameObject UIRay;

    /// <summary>
    /// Turns off all hand-held item GameObjects associated with this script.
    /// </summary>
    public void TurnOffAll()
    {
        Model?.SetActive(false);
        Katana?.SetActive(false);
        Pistol?.SetActive(false);
        UIRay?.SetActive(false);
        PistolHand?.SetActive(false);
        KatanaHand?.SetActive(false);
    }
}
