public interface ISavable
{
    void Load();
    void Save();
    string GetDataFileName();
}