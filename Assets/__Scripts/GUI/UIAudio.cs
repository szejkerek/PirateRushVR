using UnityEngine;
using UnityEngine.EventSystems;

public class UIAudio : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sound clickAudio;
    public Sound hoverEnterAudio;
    public Sound hoverExitAudio;

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Instance.PlayGlobal(clickAudio);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        AudioManager.Instance.PlayGlobal(hoverEnterAudio);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        AudioManager.Instance.PlayGlobal(hoverExitAudio);
    }
}
