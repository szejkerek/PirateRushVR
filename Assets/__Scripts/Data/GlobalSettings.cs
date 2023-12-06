using System;

[Serializable]
public class GlobalSettings : ISavable
{
    public string LastNickname = "";
    public float Volume = 1f;
    public string TurnType = "snap";
    public bool UsingVignette = false;

    public string GetDataFileName()
    {
        return "GlobalSettings.dat";
    }

    public void Save()
    {
        SaveManager<GlobalSettings>.Save(this, GetDataFileName());
    }

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