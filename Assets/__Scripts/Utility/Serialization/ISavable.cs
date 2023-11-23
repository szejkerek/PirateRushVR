public interface ISavable
{
    string SaveDataFileName { get; }

    void Load();
    void Save();
}