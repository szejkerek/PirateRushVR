using System;
using System.IO;

public static class SaveManager<T> where T : new()
{
    public static void Save(T data, string fileName)
    {
        string jsonString = JsonUtilityEx.ToJson(data, prettyPrint: true);
        FileManager.WriteToFile(fileName, jsonString);
    }

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
