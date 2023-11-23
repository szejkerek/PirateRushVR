using System;
using System.IO;
using UnityEngine;
public static class FileManager
{
    public static bool WriteToFile(string a_FileName, string a_FileContents)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);
        Debug.Log(fullPath);

        try
        {
            File.WriteAllText(fullPath, a_FileContents);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to write to {fullPath} with exception {e}");
            return false;
        }
    }

    public static bool LoadFromFile(string a_FileName, out string result)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);

        try
        {
            result = File.ReadAllText(fullPath);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogError($"Failed to read from {fullPath} with exception {e}");
            result = "";
            return false;
        }
    }
}

public static class SaveManager<T>
{
    public static void Save(T data, string fileName)
    {
        string jsonString = JsonUtilityEx.ToJson(data, prettyPrint: true);
        FileManager.WriteToFile(fileName, jsonString);
    }

    public static T Load(string fileName)
    {
        if(FileManager.LoadFromFile(fileName, out string result))
        {
            return JsonUtilityEx.FromJson<T>(result);

        }
        else
        {
            return default;
        }
    }

}