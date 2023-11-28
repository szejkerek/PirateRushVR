using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TutorialPanelSO", menuName = "TutorialPanelSO", order = 1)]
public class TutorialPanelSO : ScriptableObject
{
    [field: SerializeField] public string Title { private set; get; }
    [field: SerializeField, TextArea(3, 10)] public string Text { private set; get; }
    [field: SerializeField] public Sprite Image { private set; get; }
}
