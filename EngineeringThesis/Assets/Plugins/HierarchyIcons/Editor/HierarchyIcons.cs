using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace HierarchyIcons.Editor
{
    [InitializeOnLoad]
    class HierarchyIcons
    {
        private static IconsList list;

        static HierarchyIcons()
        {
            EditorApplication.hierarchyWindowItemOnGUI += HandleHierarchyWindowItemOnGUI;
        }

        private static Vector2 offset = new Vector2(18, 0);


        private static Dictionary<string, GUIContent> iconsNames = new Dictionary<string, GUIContent>();
        private static GUIContent tmpText;

        private static Dictionary<Transform, bool> openedFolders = new Dictionary<Transform, bool>(); 

        private static void HandleHierarchyWindowItemOnGUI(int instanceID, Rect selectionRect)
        {
            var obj = EditorUtility.InstanceIDToObject(instanceID);

            if (Event.current.type == EventType.Layout)
            {
                openedFolders = new Dictionary<Transform, bool>();
                CheckFolders(obj);
            }

            if (Event.current.type == EventType.Repaint)
            {
                Repaint(obj, selectionRect);
            }
        }

        public static void CheckFolders(Object obj)
        {
            if (obj != null)
            {
                var go = (obj as GameObject);
                var parent = go.transform.parent;
                if (parent != null && parent.GetComponent<Folder>())
                {
                    if (!openedFolders.ContainsKey(parent))
                    {
                        openedFolders.Add(parent, true);
                    }
                }
            }
        }

        public static void Repaint(Object obj, Rect selectionRect)
        {
            if (CreateSingleton())
            {
                if (obj != null)
                {
                    var gameObject = (obj as GameObject);
                    selectionRect.position += offset;

                    var iconPos = GetIconPos(selectionRect);
                    bool isfolder = false;

                    var itemName = gameObject.transform.name.ToLower();
                
                    if (gameObject.GetComponent<Folder>())
                    {
                        RepaintFolders(gameObject,iconPos);
                        isfolder = true;
                    }
                    else if (gameObject.TryGetComponent(out Light light))
                    {
                        GUI.Label(iconPos, DrawLightIcon(light.type));
                    }
                    else if (itemName.Contains("manager") || itemName.Contains("context") || itemName.Contains("controller") || itemName.Contains("service"))
                    {
                        GUI.Label(iconPos, EditorGUIUtility.IconContent("GameManager Icon"));
                    }
                    else if (gameObject.GetComponent<TMP_Text>())
                    {
                        iconPos.size = Vector2.one * 18;
                        iconPos.position += new Vector2(3, 2.5f);
                        GUI.Label(iconPos, tmpText);
                    }
                    else
                    {
                        RepaintComponent(gameObject, iconPos);
                    }

                    if (list.allEmptyFolders && !isfolder && gameObject.transform.localPosition == Vector3.zero)
                    {
                        if (gameObject.transform.GetComponents<Component>().Length == 1)
                        {
                            gameObject.AddComponent<Folder>();
                        }
                    }
                }
            }
        }

        public static void RepaintFolders(GameObject go, Rect iconPos)
        {
            GUIContent folder;
            if (go.transform.childCount == 0)
            {
                folder = EditorGUIUtility.IconContent("d_FolderEmpty Icon");
                GUI.Label(iconPos, folder);
            }
            else
            {
                if (openedFolders.ContainsKey(go.transform))
                {
                    folder = EditorGUIUtility.IconContent("Folder On Icon");
                    GUI.Label(iconPos, folder);
                }
                else
                {
                    folder = EditorGUIUtility.IconContent("Folder On Icon");
                    GUI.Label(iconPos, folder);
                    folder = EditorGUIUtility.IconContent("Folder Icon");
                    GUI.Label(new Rect(iconPos.position.x+ 1, iconPos.position.y + 1, iconPos.size.x, iconPos.size.y), folder);
                
                }
            }

        }

        public static void RepaintComponent(GameObject go,  Rect iconPos)
        {
            for (int i = 0; i < list.icons.Count; i++)
            {

                var type = list.icons[i];
                if (go.GetComponent(type))
                {
                    iconPos.size = Vector2.one * 20;
                    iconPos.position -= Vector2.down * 2.5f;
                    iconPos.position += Vector2.right * 2f;
                    var key = $"d_{list.icons[i]} Icon";
                    if (!iconsNames.ContainsKey(key))
                    {
                        var icon = EditorGUIUtility.IconContent(key);
                        if (icon != null)
                        {
                            iconsNames.Add(key, icon);
                        }
                        else
                        {
                            list.icons.RemoveAt(i);
                            return;
                        }
                    }
                    else
                    {
                        GUI.Label(iconPos, iconsNames[key]);
                    }

                    return;
                }
            }

        }

        public static bool CreateSingleton()
        {
            if (list == null)
            {
                var find = AssetDatabase.FindAssets("t:IconsList");
                if (find.Length != 0)
                {
                    var path = AssetDatabase.GUIDToAssetPath(find[0]);
                    list = AssetDatabase.LoadAssetAtPath<IconsList>(path);


                    var tmptextIcons = AssetDatabase.FindAssets("TMP - Text Component Icon");
                    if (tmptextIcons.Length != 0)
                    {
                        var tmpIcon = AssetDatabase.LoadAssetAtPath<Texture2D>(AssetDatabase.GUIDToAssetPath(tmptextIcons[0]));
                        tmpText = new GUIContent(tmpIcon);
                    }
                
                }
            }

            return list != null;
        }
    
        public static GUIContent DrawLightIcon(LightType lightType)
        {
            var type = "";
            switch (lightType)
            {
                case LightType.Directional:
                    type = "d_DirectionalLight Icon";
                    break;
                case LightType.Spot:
                    type = "d_Spotlight Icon";
                    break;
                default:
                    type = "d_Light Icon";
                    break;

            }

            return EditorGUIUtility.IconContent(type);
        }

        public static Rect GetIconPos(Rect selectionRect)
        {
            var iconPos = selectionRect;
            iconPos.position += new Vector2(selectionRect.width / 1.25f, 0);
            iconPos.position -= Vector2.up * 5;
            iconPos.size = new Vector2(25, 25);

            return iconPos;
        }

    }
}