// Copyright (c) Bartłomiej Gordon 2023. All rights reserved.
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public static class Extensions
{
    public static List<T> SelectRandomElements<T>(this List<T> list, int count)
    {     
        List<T> copiedList = new List<T>(list);
        List<T> selectedElements = new List<T>();

        if(selectedElements.Count > count)
        {
            Debug.LogWarning("Number of selected elements is greater than given list!");
            return copiedList;
        }
        

        while(selectedElements.Count < count)
        {
            int randomIndex = Random.Range(0, copiedList.Count);

            selectedElements.Add(copiedList[randomIndex]);
            copiedList.RemoveAt(randomIndex);
        }

        return selectedElements;
    }
}
