using System.Linq;
using UnityEditor;
using UnityEngine;

public class FindMissingScripts
{
    [MenuItem("My menu/Find missing scripts (PROJECT)")]
    static void FindMissingScriptsInProject()
    {
        string[] prefabPaths = AssetDatabase.GetAllAssetPaths().Where(path => path.EndsWith(".prefab", System.StringComparison.OrdinalIgnoreCase)).ToArray();

        foreach (string prefabPath in prefabPaths)
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);

            foreach(Component component in prefab.GetComponentsInChildren<Component>())
            {
                if(component == null)
                {
                    Debug.LogError("Prefab found with missing script " + prefabPath, prefab);
                }
            }
        }
    }

    [MenuItem("My menu/Find missing scripts (SCENE)")]
    static void FindMissingScriptsInScene()
    {
        foreach(GameObject gameObject in GameObject.FindObjectsOfType<GameObject>())
        {
            foreach (Component component in gameObject.GetComponentsInChildren<Component>())
            {
                if (component == null)
                {
                    Debug.LogError("GameObject found with missing script " + gameObject.name);
                }
            }
        }
    }
}
