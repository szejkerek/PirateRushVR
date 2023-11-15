using System;
using UnityEngine;

[Serializable]
public class HandItems
{
    public GameObject Katana;
    public GameObject Pistol;
    public GameObject TeleportRay;
    public GameObject UIRay;

    public void TurnOffAll()
    {
        Katana?.SetActive(false);
        Pistol?.SetActive(false);
        TeleportRay?.SetActive(false);
        UIRay?.SetActive(false);
    }
}
