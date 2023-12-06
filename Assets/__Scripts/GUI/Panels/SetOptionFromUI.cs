using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SetOptionFromUI : MonoBehaviour
{
    [SerializeField] Slider volumeSlider;
    [SerializeField] TMP_Dropdown turnDropdown;
    [SerializeField] Toggle vignetteToggle;

    private void Start()
    {
        UpdateSettingsValue();
        volumeSlider.onValueChanged.AddListener(SetGlobalVolume);
        turnDropdown.onValueChanged.AddListener(SetTurnPlayerPref);
        vignetteToggle.onValueChanged.AddListener(SetVignetteUsage);
    }

    private void SetVignetteUsage(bool arg)
    {
        FindObjectOfType<SetPlayerPreferences>()?.SetVignetteUsage(arg);
        GlobalSettingManager.Instance.SetVignetteUsage(arg);
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

        float volume = GlobalSettingManager.Instance.GetVolume();
        volumeSlider.SetValueWithoutNotify(volume * 100);

        vignetteToggle.SetIsOnWithoutNotify(GlobalSettingManager.Instance.GetVignetteUsage());
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
