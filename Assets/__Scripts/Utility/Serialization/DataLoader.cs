using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// Static class responsible for loading assets of a specified type.
/// </summary>
/// <typeparam name="T">The type of assets to load.</typeparam>
public static class DataLoader<T> where T : Object
{
    /// <summary>
    /// Loads assets of type T using the provided label reference.
    /// </summary>
    /// <param name="label">The label reference for the assets to load.</param>
    /// <returns>A list of loaded assets of type T.</returns>
    public static List<T> Load(AssetLabelReference label)
    {
        List<T> result = new List<T>();
        var loadOperation = Addressables.LoadAssetsAsync<T>(label, result.Add).WaitForCompletion();

        if (result.Count == 0)
        {
            Debug.LogError($"Did not load list for {label.labelString} path");
            return null;
        }

        return result;
    }
}
