using System;
using System.Collections;
using UnityEngine;

/// <summary>
/// Enum representing different scenes in the game.
/// </summary>
public enum SceneEnum
{
    MainMenu,
    Gameplay,
}

/// <summary>
/// Singleton class responsible for loading scenes.
/// </summary>
public class SceneLoader : Singleton<SceneLoader>
{
    /// <summary>
    /// Action triggered when the scene is changed.
    /// </summary>
    public Action OnSceneChanged;

    /// <summary>
    /// Action triggered when the scene is fully loaded.
    /// </summary>
    public Action OnSceneFullyLoaded;

    FadeScreen fadeScreen;

    /// <summary>
    /// Loads the scene using the provided scene index.
    /// </summary>
    /// <param name="sceneIndex">The index of the scene to load.</param>
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(LoadSceneRoutine(sceneIndex));
    }

    /// <summary>
    /// Loads the scene using the provided scene enum.
    /// </summary>
    /// <param name="sceneEnum">The enum value representing the scene to load.</param>
    public void LoadScene(SceneEnum sceneEnum)
    {
        int sceneIndex = (int)sceneEnum;
        LoadScene(sceneIndex);
    }

    /// <summary>
    /// Coroutine that loads the scene.
    /// </summary>
    /// <param name="sceneIndex">The index of the scene to load.</param>
    IEnumerator LoadSceneRoutine(int sceneIndex)
    {
        fadeScreen = FindObjectOfType<FadeScreen>();
        if (fadeScreen == null)
        {
            Debug.LogError("Couldn't get screen fader in scene!");
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);
        }
        else
        {
            fadeScreen.FadeOut();
            yield return new WaitForSeconds(fadeScreen.FadeDuration);

            AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneIndex);
            operation.allowSceneActivation = false;

            float timer = 0f;
            while (timer <= fadeScreen.FadeDuration && !operation.isDone)
            {
                timer += Time.deltaTime;
                yield return null;
            }

            OnSceneChanged?.Invoke();
            operation.allowSceneActivation = true;
            fadeScreen.FadeIn();
        }
    }
}
