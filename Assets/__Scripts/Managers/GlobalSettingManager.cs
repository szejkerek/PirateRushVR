using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Enum defining different types of turns.
/// </summary>
[Serializable]
public enum TurnType
{
    Snap,
    Continuous
}

/// <summary>
/// Manages global settings and configurations for the game.
/// </summary>
public class GlobalSettingManager : Singleton<GlobalSettingManager>
{
    GlobalSettings currentSettings = new GlobalSettings();

    protected override void Awake()
    {
        base.Awake();
        currentSettings.Load();
    }

    /// <summary>
    /// Sets the nickname for the player.
    /// </summary>
    public void SetNickname(string newNickname)
    {
        currentSettings.LastNickname = newNickname;
        currentSettings.Save();
    }

    /// <summary>
    /// Sets the type of turn (Snap or Continuous).
    /// </summary>
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

    /// <summary>
    /// Sets the usage of vignette effect.
    /// </summary>
    public void SetVignetteUsage(bool isUsing)
    {
        currentSettings.UsingVignette = isUsing;
        currentSettings.Save();
    }

    /// <summary>
    /// Sets the volume level.
    /// </summary>
    public void SetVolume(float volume)
    {
        currentSettings.Volume = volume;
        currentSettings.Save();
    }

    /// <summary>
    /// Retrieves the stored player nickname.
    /// </summary>
    public string GetNickname()
    {
        currentSettings.Load();
        return currentSettings.LastNickname;
    }

    /// <summary>
    /// Retrieves the stored turn type.
    /// </summary>
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

    /// <summary>
    /// Retrieves the status of vignette usage.
    /// </summary>
    public bool GetVignetteUsage()
    {
        currentSettings.Load();
        return currentSettings.UsingVignette;
    }

    /// <summary>
    /// Retrieves the stored volume level.
    /// </summary>
    public float GetVolume()
    {
        currentSettings.Load();
        return currentSettings.Volume;
    }
}
