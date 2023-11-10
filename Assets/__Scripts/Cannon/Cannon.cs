using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public ComboController ComboManager => comboManager;
    ComboController comboManager;

    public CannonShooting Luncher => luncher;
    private CannonShooting luncher;

    

    private void Awake()
    {
        luncher = GetComponent<CannonShooting>();     
        comboManager = GetComponent<ComboController>();
    }

    public void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
