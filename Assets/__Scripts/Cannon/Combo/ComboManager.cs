using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    ComboItemFactory comboItemFactory;
    Queue<ComboItem> comboItems = new Queue<ComboItem>();

    private void Awake()
    {
        comboItemFactory = new ComboItemFactory(this);
    }

    public void UpdateOnTick()
    {

    }
}
