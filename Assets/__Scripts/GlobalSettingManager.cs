using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalSettingManager : MonoBehaviour
{
    GlobalSettings currentSettings;

    private void Awake()
    {
        currentSettings.Load();
    }

    public void SetNickname(string newNickname)
    {
        currentSettings.LastNickname = newNickname;
        currentSettings.Save();
    }

    public void SetTurnType(string turnType)
    {
        if (turnType != "snap" || turnType != "continous")
            return;

        currentSettings.TurnType = turnType;
        currentSettings.Save();
    }

    public void SetVignieteUsage(bool isUsing)
    {
        currentSettings.UsingVigniete = isUsing;
        currentSettings.Save();
    }

    public void SetVolume(float volume)
    {
        volume = Mathf.Clamp01(volume);
        currentSettings.Volume = volume;
        currentSettings.Save();
    }

    public string GetNickname()
    {
        return currentSettings.LastNickname;
    }
    public string GetTurnType()
    {
        return currentSettings.TurnType;
    }

    public bool GetVignetteUsage()
    {
        return currentSettings.UsingVigniete;
    }
    public float GetVolume()
    {
        return currentSettings.Volume;
    }

}
