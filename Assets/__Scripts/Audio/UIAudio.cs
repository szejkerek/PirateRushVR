using UnityEngine;
using UnityEngine.EventSystems;

public class UIAudio : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sound clickAudio;
    public Sound hoverEnterAudio;
    public Sound hoverExitAudio;

    public void OnPointerClick(PointerEventData eventData)
    {
        if (clickAudio != null)
            AudioManager.Instance.PlayGlobal(clickAudio);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (hoverEnterAudio != null)
            AudioManager.Instance.PlayGlobal(hoverEnterAudio);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (hoverExitAudio != null)
            AudioManager.Instance.PlayGlobal(hoverExitAudio);
    }
}
