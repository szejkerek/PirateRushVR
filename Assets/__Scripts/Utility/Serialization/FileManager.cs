using System;
using System.IO;
using UnityEngine;

/// <summary>
/// Static class responsible for file operations.
/// </summary>
public static class FileManager
{
    /// <summary>
    /// Writes content to a file.
    /// </summary>
    /// <param name="a_FileName">The name of the file to write to.</param>
    /// <param name="a_FileContents">The contents to be written to the file.</param>
    /// <returns>True if writing to the file was successful, otherwise false.</returns>
    public static bool WriteToFile(string a_FileName, string a_FileContents)
    {
        var fullPath = Path.Combine(Application.persistentDataPath, a_FileName);
        try
        {
            File.WriteAllText(fullPath, a_FileContents);
            return true;
        }
        catch (Exception e)
        {
            Debug.LogWarning($"Failed to write to {fullPath} with exception {e}");
            return false;
        }
    }

    /// <summary>
    /// Loads content from a file.
    /// </summary>
    /// <param name="a_FileName">The name of the file to read from.</param>
    /// <param name="result">The content read from the file.</param>
    /// <returns>True if reading from the file was successful, otherwise false.</returns>
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
            Debug.LogWarning($"Failed to read from {fullPath} with exception {e}");
            result = "";
            return false;
        }
    }
}
