using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Controls the behavior of UI buttons, changing their appearance and playing audio on different interactions.
/// </summary>
public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] ButtonTheme theme;

    Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    /// <summary>
    /// Handles the button's behavior when clicked.
    /// </summary>
    /// <param name="eventData">The pointer event data.</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.Instance.PlayGlobal(theme.ClickAudio);
        image.sprite = theme.ClickTheme;
    }

    /// <summary>
    /// Handles the button's behavior when the pointer enters.
    /// </summary>
    /// <param name="eventData">The pointer event data.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlayGlobal(theme.HoverEnterAudio);
        image.sprite = theme.HoverTheme;
    }

    /// <summary>
    /// Handles the button's behavior when the pointer exits.
    /// </summary>
    /// <param name="eventData">The pointer event data.</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = theme.MainTheme;
    }

    /// <summary>
    /// Handles the button's behavior when the pointer is released.
    /// </summary>
    /// <param name="eventData">The pointer event data.</param>
    public void OnPointerUp(PointerEventData eventData)
    {
        image.sprite = theme.HoverTheme;
    }
}
