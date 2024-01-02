// Copyright (c) Bartłomiej Gordon 2023. All rights reserved.

using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// Extension methods for lists.
/// </summary>
public static class Extensions
{
    /// <summary>
    /// Selects a specified number of random elements from the list.
    /// </summary>
    /// <typeparam name="T">Type of elements in the list.</typeparam>
    /// <param name="list">The list to select elements from.</param>
    /// <param name="count">The number of elements to select.</param>
    /// <returns>A list containing randomly selected elements.</returns>
    public static List<T> SelectRandomElements<T>(this List<T> list, int count)
    {
        List<T> copiedList = new List<T>(list);
        List<T> selectedElements = new List<T>();

        if (selectedElements.Count > count)
        {
            Debug.LogWarning("Number of selected elements is greater than given list!");
            return copiedList;
        }

        while (selectedElements.Count < count)
        {
            int randomIndex = Random.Range(0, copiedList.Count);
            selectedElements.Add(copiedList[randomIndex]);
            copiedList.RemoveAt(randomIndex);
        }

        return selectedElements;
    }

    /// <summary>
    /// Selects a single random element from the list.
    /// </summary>
    /// <typeparam name="T">Type of elements in the list.</typeparam>
    /// <param name="list">The list to select an element from.</param>
    /// <returns>A randomly selected element from the list.</returns>
    public static T SelectRandomElement<T>(this List<T> list)
    {
        if (list == null || list.Count == 0)
        {
            Debug.LogWarning("The list is empty or null.");
        }

        int randomIndex = Random.Range(0, list.Count);
        return list[randomIndex];
    }
}
