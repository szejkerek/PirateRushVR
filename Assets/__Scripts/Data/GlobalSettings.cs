using System;

/// <summary>
/// Represents global settings that can be saved and loaded.
/// Implements the ISavable interface.
/// </summary>
[Serializable]
public class GlobalSettings : ISavable
{
    /// <summary>
    /// The last used nickname.
    /// </summary>
    public string LastNickname = "";

    /// <summary>
    /// The volume setting.
    /// </summary>
    public float Volume = 1f;

    /// <summary>
    /// The type of turn (e.g., 'snap' or other).
    /// </summary>
    public string TurnType = "snap";

    /// <summary>
    /// Indicates whether vignette is being used.
    /// </summary>
    public bool UsingVignette = false;

    /// <summary>
    /// Retrieves the data file name used for saving/loading.
    /// </summary>
    /// <returns>The data file name.</returns>
    public string GetDataFileName()
    {
        return "GlobalSettings.dat";
    }

    /// <summary>
    /// Saves the global settings.
    /// </summary>
    public void Save()
    {
        SaveManager<GlobalSettings>.Save(this, GetDataFileName());
    }

    /// <summary>
    /// Loads the global settings.
    /// </summary>
    public void Load()
    {
        GlobalSettings loaded = SaveManager<GlobalSettings>.Load(GetDataFileName());
        LastNickname = loaded.LastNickname;
        Volume = loaded.Volume;
        TurnType = loaded.TurnType;
        UsingVignette = loaded.UsingVignette;
        Save();
    }
}
