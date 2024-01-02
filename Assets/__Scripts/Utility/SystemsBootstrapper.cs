using UnityEngine;
using UnityEngine.AddressableAssets;

/// <summary>
/// Static class responsible for system bootstrapping.
/// </summary>
public static class SystemBootstrapper
{
    /// <summary>
    /// Method executed before the scene load to instantiate persistent systems.
    /// </summary>
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    public static void Execute()
    {
        // Instantiate and mark as persistent the object named "PersistentSystems" loaded via Addressables.
        Object.DontDestroyOnLoad(Addressables.InstantiateAsync("PersistentSystems").WaitForCompletion());
    }
}
