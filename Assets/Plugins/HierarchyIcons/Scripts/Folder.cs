using UnityEngine;

namespace HierarchyIcons
{
    [ExecuteAlways]
    public class Folder : MonoBehaviour
    {

#if UNITY_EDITOR

        [SerializeField] private bool haveZeroPos;

        private void LateUpdate()
        {
            if (!Application.isPlaying && haveZeroPos)
            {
                transform.localPosition = Vector3.zero;
            }
        }


        private void OnDrawGizmos()
        {
            var filters = GetComponentsInChildren<Renderer>();
            if (filters.Length != 0)
            {
                float count = 0;
                Vector3 center = new Vector3();
                for (int i = 0; i < filters.Length; i++)
                {
                    center += (filters[i].bounds.center);
                    count++;
                }

                Gizmos.DrawIcon(center / count, "Folder Icon");
            }
            else
            {
                Gizmos.DrawIcon(transform.position, "Folder Icon");
            }
        }
        
#endif
    }
}