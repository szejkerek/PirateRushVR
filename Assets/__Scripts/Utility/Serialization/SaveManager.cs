using System;
using System.IO;

/// <summary>
/// Static class responsible for saving and loading data of type T.
/// </summary>
/// <typeparam name="T">The type of data to be saved and loaded.</typeparam>
public static class SaveManager<T> where T : new()
{
    /// <summary>
    /// Saves the provided data to a file.
    /// </summary>
    /// <param name="data">The data to be saved.</param>
    /// <param name="fileName">The name of the file to save the data to.</param>
    public static void Save(T data, string fileName)
    {
        string jsonString = JsonUtilityEx.ToJson(data, prettyPrint: true);
        FileManager.WriteToFile(fileName, jsonString);
    }

    /// <summary>
    /// Loads data from a file.
    /// </summary>
    /// <param name="fileName">The name of the file to load data from.</param>
    /// <returns>The loaded data of type T.</returns>
    public static T Load(string fileName)
    {
        try
        {
            if (FileManager.LoadFromFile(fileName, out string result))
            {
                return JsonUtilityEx.FromJson<T>(result);
            }
            else
            {
                throw new IOException("Failed to load data from file.");
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine("Error while loading data: " + ex.Message);
            return new T();
        }
    }
}
