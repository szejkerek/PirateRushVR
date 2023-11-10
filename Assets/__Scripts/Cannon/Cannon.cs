using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public ComboController ComboManager => comboManager;
    ComboController comboManager;

    private void Awake()
    {
        comboManager = GetComponent<ComboController>();
    }
}
