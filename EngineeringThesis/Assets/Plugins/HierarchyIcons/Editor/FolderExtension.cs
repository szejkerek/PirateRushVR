using UnityEditor;
using UnityEngine;

namespace HierarchyIcons.Editor
{
    public static class FolderExtension
    {
        private static float _lastMenuCallTimestamp = 0f;
        
        [MenuItem("GameObject/Create Folder", isValidateFunction: false, priority = -998)]
        public static void CreateFolder()   
        {
            if (Time.unscaledTime.Equals(_lastMenuCallTimestamp)) return;
           
            CreateFolderProcess();
            
            _lastMenuCallTimestamp = Time.unscaledTime;
        }
        private static void CreateFolderProcess()
        {
            var folder = new GameObject("Folder").AddComponent<Folder>();
            Undo.RegisterCreatedObjectUndo(folder.gameObject, "Folder Creation");
            if (Selection.gameObjects.Length == 1)
            {
                folder.transform.parent = Selection.gameObjects[0].transform;
                if (folder.transform.parent.GetComponentInParent<Canvas>()) folder.gameObject.AddComponent<RectTransform>();
            }
            Selection.activeGameObject = folder.gameObject;
            EditorUtility.SetDirty(folder);
        }

        [MenuItem("GameObject/Create Folder", isValidateFunction: true)]
        public static bool CreateFolderValidate()
        {
            return Selection.gameObjects.Length == 1 || Selection.gameObjects.Length == 0;
        }
        
        
        
        
        [MenuItem("GameObject/Create Parent Folder", isValidateFunction: false, priority = -999)]
        public static void CreateFolderParent()   
        {
            if (Time.unscaledTime.Equals(_lastMenuCallTimestamp)) return;
           
            CreateFolderProcessParent();
            
            _lastMenuCallTimestamp = Time.unscaledTime;
        }
        private static void CreateFolderProcessParent()
        {
            var folder = new GameObject("Folder").AddComponent<Folder>();
            
            
            folder.transform.parent = Selection.gameObjects[0].transform.parent;
            if (folder.transform.parent != null && folder.transform.parent.GetComponentInParent<Canvas>())
            {
                folder.gameObject.AddComponent<RectTransform>();
                folder.transform.localScale = Vector3.one;
            }


            Vector3 pos = Vector3.zero;
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                pos += Selection.gameObjects[i].transform.position;
            }

            pos /= Selection.gameObjects.Length;

            folder.transform.position = pos;
            
            for (int i = 0; i < Selection.gameObjects.Length; i++)
            {
                Undo.RegisterFullObjectHierarchyUndo(Selection.gameObjects[i], "");
                Selection.gameObjects[i].transform.parent = folder.transform;
            }
            
            Undo.RegisterCreatedObjectUndo(folder.gameObject, "Folder Creation");
            
            
            EditorUtility.SetDirty(folder);
            Selection.activeGameObject = folder.gameObject;
        }

        [MenuItem("GameObject/Create Parent Folder", isValidateFunction: true)]
        public static bool CreateFolderValidateParent()
        {
            for (int i = 0; i < Selection.gameObjects.Length-1; i++)
            {
                if (Selection.gameObjects[i].transform.parent != Selection.gameObjects[i + 1].transform.parent)
                {
                    return false;
                }
            }
            return Selection.gameObjects.Length >= 1;
        }
    }
}