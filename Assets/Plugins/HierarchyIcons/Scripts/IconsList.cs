using System.Collections.Generic;
using UnityEngine;

namespace HierarchyIcons
{
    [CreateAssetMenu(fileName = "", menuName = "Editor/Icons List", order = 1)]
    public class IconsList : ScriptableObject
    {
        public List<string> icons = new List<string>();
        public bool allEmptyFolders;
    }
}
