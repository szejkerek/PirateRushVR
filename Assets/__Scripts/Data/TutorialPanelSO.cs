using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject representing a tutorial panel with title, text, and an image.
/// </summary>
[CreateAssetMenu(fileName = "TutorialPanelSO", menuName = "TutorialPanelSO", order = 1)]
public class TutorialPanelSO : ScriptableObject
{
    /// <summary>
    /// Title of the tutorial panel.
    /// </summary>
    [field: SerializeField]
    public string Title { private set; get; }

    /// <summary>
    /// Text content of the tutorial panel.
    /// </summary>
    [field: SerializeField, TextArea(3, 10)]
    public string Text { private set; get; }

    /// <summary>
    /// Image displayed in the tutorial panel.
    /// </summary>
    [field: SerializeField]
    public Sprite Image { private set; get; }
}
