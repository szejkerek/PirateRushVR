/// <summary>
/// Interface for objects that can be saved and loaded.
/// </summary>
public interface ISavable
{
    /// <summary>
    /// Loads data for the object.
    /// </summary>
    void Load();

    /// <summary>
    /// Saves data for the object.
    /// </summary>
    void Save();

    /// <summary>
    /// Gets the data file name associated with the object.
    /// </summary>
    /// <returns>The name of the data file for this object.</returns>
    string GetDataFileName();
}
