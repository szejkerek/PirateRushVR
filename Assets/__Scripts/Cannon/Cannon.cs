using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public ComboController ComboManager => comboManager;
    ComboController comboManager;

    public CannonShooting Launcher => launcher;
    private CannonShooting launcher;

    

    private void Awake()
    {
        launcher = GetComponent<CannonShooting>();     
        comboManager = GetComponent<ComboController>();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
