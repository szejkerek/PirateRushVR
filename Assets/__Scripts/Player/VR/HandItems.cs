using System;
using UnityEngine;

public class HandItems: MonoBehaviour
{
    public GameObject Model;
    public GameObject Katana;
    public GameObject KatanaHand;
    public GameObject Pistol;
    public GameObject PistolHand;
    public GameObject UIRay;

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
