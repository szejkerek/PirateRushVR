using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] ButtonTheme theme;

    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        AudioManager.Instance.PlayGlobal(theme.ClickAudio);
        image.sprite = theme.ClickTheme;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlayGlobal(theme.HoverEnterAudio);
        image.sprite = theme.HoverTheme;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.sprite = theme.MainTheme;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.sprite = theme.HoverTheme;
    }
}
