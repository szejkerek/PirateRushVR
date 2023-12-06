using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SetOptionFromUI : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] TMP_Dropdown turnDropdown;

    private void Start()
    {
        UpdateSettingsValue();
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        turnDropdown.onValueChanged.AddListener(SetTurnPlayerPref);
    }

    private void UpdateSettingsValue()
    {
        TurnType type = GlobalSettingManager.Instance.GetTurnType();
        switch (type)
        {
            case TurnType.Snap:
                turnDropdown.SetValueWithoutNotify(0);               
                break;
            case TurnType.Continuous:
                turnDropdown.SetValueWithoutNotify(1);
                break;
        }
        FindObjectOfType<SetPlayerPreferences>()?.SetTurnType(type);


        float volume = GlobalSettingManager.Instance.GetVolume();
        AudioManager.Instance.SetVolume(volume);
        volumeSlider.SetValueWithoutNotify(volume * 100);
    }

    public void SetGlobalVolume(float value)
    {
        float normalizedValue = value / 100f;
        AudioManager.Instance.SetVolume(normalizedValue);
        GlobalSettingManager.Instance.SetVolume(normalizedValue);
    }

    public void SetTurnPlayerPref(int value)
    {
        TurnType type;
        switch (value)
        {
            case 0:
                type = TurnType.Snap;
                break;
            case 1:
                type = TurnType.Continuous;
                break;
            default:
                type = TurnType.Snap;
                break;
        }
        FindObjectOfType<SetPlayerPreferences>()?.SetTurnType(type);
        GlobalSettingManager.Instance.SetTurnType(type);
    }
}
