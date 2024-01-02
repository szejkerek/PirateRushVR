using UnityEngine;

/// <summary>
/// A base class ensuring a single static instance for derived classes.
/// </summary>
/// <typeparam name="T">The type of MonoBehaviour to be used as a singleton.</typeparam>
public abstract class StaticInstance<T> : MonoBehaviour where T : MonoBehaviour
{
    /// <summary>
    /// The static instance of type T.
    /// </summary>
    public static T Instance { get; private set; }

    /// <summary>
    /// Called when the script instance is being loaded.
    /// </summary>
    protected virtual void Awake()
    {
        Instance = this as T;
    }

    /// <summary>
    /// Called when the application quits.
    /// </summary>
    protected virtual void OnApplicationQuit()
    {
        Instance = null;
        Destroy(gameObject);
    }
}

/// <summary>
/// A base class ensuring a single static instance for derived classes as a Singleton.
/// </summary>
/// <typeparam name="T">The type of MonoBehaviour to be used as a singleton.</typeparam>
public abstract class Singleton<T> : StaticInstance<T> where T : MonoBehaviour
{
    /// <summary>
    /// Called when the script instance is being loaded.
    /// </summary>
    protected override void Awake()
    {
        if (Instance != null)
        {
            return;
        }
        base.Awake();
    }
}
