using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIAudio : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    public Sound clickAudio;
    public Sound hoverEnterAudio;
    public Sound hoverExitAudio;

    public Sprite mainTheme;
    public Sprite hoverTheme;
    public Sprite clickTheme;

    Image image;
    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickAudio != null)
            AudioManager.Instance.PlayGlobal(clickAudio);

    }

    public void OnPointerDown(PointerEventData eventData)
    {
        image.sprite = clickTheme;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverEnterAudio != null)
            AudioManager.Instance.PlayGlobal(hoverEnterAudio);

        image.sprite = hoverTheme;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverExitAudio != null)
            AudioManager.Instance.PlayGlobal(hoverExitAudio);

        image.sprite = mainTheme;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        image.sprite = hoverTheme;
    }
}
