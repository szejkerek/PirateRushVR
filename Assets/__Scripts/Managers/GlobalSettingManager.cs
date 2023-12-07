using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum TurnType
{
    Snap,
    Continuous
}

public class GlobalSettingManager : Singleton<GlobalSettingManager>
{
    GlobalSettings currentSettings = new GlobalSettings();

    protected override void Awake()
    {
        base.Awake();
        currentSettings.Load();
    }

    public void SetNickname(string newNickname)
    {
        currentSettings.LastNickname = newNickname;
        currentSettings.Save();
    }

    public void SetTurnType(TurnType turnType)
    {
        switch (turnType)
        {
            case TurnType.Snap:
                currentSettings.TurnType = "snap";
                break;
            case TurnType.Continuous:
                currentSettings.TurnType = "continuous";
                break;
        }
        currentSettings.Save();
    }

    public void SetVignetteUsage(bool isUsing)
    {
        currentSettings.UsingVignette = isUsing;
        currentSettings.Save();
    }

    public void SetVolume(float volume)
    {
        currentSettings.Volume = volume;
        currentSettings.Save();
    }

    public string GetNickname()
    {
        currentSettings.Load();
        return currentSettings.LastNickname;
    }
    public TurnType GetTurnType()
    {
        currentSettings.Load();
        switch (currentSettings.TurnType)
        {
            case "snap": return TurnType.Snap;
            case "continuous": return TurnType.Continuous;
            default: return TurnType.Snap;
        }
    }

    public bool GetVignetteUsage()
    {
        currentSettings.Load();
        return currentSettings.UsingVignette;
    }
    public float GetVolume()
    {
        currentSettings.Load();
        return currentSettings.Volume;
    }

}
