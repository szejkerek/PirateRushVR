using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ScriptableObject representing the theme settings for a button.
/// </summary>
[CreateAssetMenu(fileName = "ButtonTheme", menuName = "ButtonTheme", order = 1)]
public class ButtonTheme : ScriptableObject
{
    /// <summary>
    /// Audio clip for button click.
    /// </summary>
    [field: Header("Audio")]
    [field: SerializeField] public Sound ClickAudio { private set; get; }

    /// <summary>
    /// Audio clip for button hover enter.
    /// </summary>
    [field: SerializeField] public Sound HoverEnterAudio { private set; get; }

    /// <summary>
    /// Main sprite theme for the button.
    /// </summary>
    [field: Header("Theme")]
    [field: SerializeField] public Sprite MainTheme { private set; get; }

    /// <summary>
    /// Sprite theme for button click.
    /// </summary>
    [field: SerializeField] public Sprite ClickTheme { private set; get; }

    /// <summary>
    /// Sprite theme for button hover.
    /// </summary>
    [field: SerializeField] public Sprite HoverTheme { private set; get; }
}
