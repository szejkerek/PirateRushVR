using System;
using System.Collections;
using UnityEngine;

public enum SceneEnum
{
    MainMenu,
    Gameplay,
}

public class SceneLoader : Singleton<SceneLoader>
{
    public Action OnSceneChanged;
    public Action OnSceneFullyLoaded;
    FadeScreen fadeScreen;

    public void LoadScene(int sceneIndex)
    {       
        StartCoroutine(LoadSceneRoutine(sceneIndex));
    }

    public void LoadScene(SceneEnum sceneEnum)
    {
        int sceneIndex = (int)sceneEnum;
        LoadScene(sceneIndex);
    }

    IEnumerator LoadSceneRoutine(int sceneIndex)
    {
        fadeScreen = FindObjectOfType<FadeScreen>();
        if(fadeScreen == null)
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