using System;
using UnityEngine;

[Serializable]
public class HandItems
{
    public GameObject Model;
    public GameObject Katana;
    public GameObject Pistol;
    public GameObject UIRay;

    public void TurnOffAll()
    {
        Model?.SetActive(false);
        Katana?.SetActive(false);
        Pistol?.SetActive(false);
        UIRay?.SetActive(false);
    }
}
