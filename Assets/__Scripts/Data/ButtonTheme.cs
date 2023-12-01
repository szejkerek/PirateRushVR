using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ButtonTheme", menuName = "ButtonTheme", order = 1)]
public class ButtonTheme : ScriptableObject
{
    [field: Header("Audio")]
    [field: SerializeField] public Sound ClickAudio { private set; get; }
    [field: SerializeField] public Sound HoverEnterAudio { private set; get; }

    [field: Header("Theme")]
    [field: SerializeField] public Sprite MainTheme { private set; get; }
    [field: SerializeField] public Sprite ClickTheme { private set; get; }
    [field: SerializeField] public Sprite HoverTheme { private set; get; }
}
