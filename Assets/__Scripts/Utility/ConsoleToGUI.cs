using System.Collections.Generic;
using TMPro;
using UnityEngine;

/// <summary>
/// Displays console logs in a TMP_Text component in the UI.
/// </summary>
public class ConsoleToGUI : MonoBehaviour
{
    public TMP_Text text;
    public int lineCount = 35;
    Queue<string> lines = new Queue<string>();

    void OnEnable()
    {
        Application.logMessageReceived += Log;
    }

    void OnDisable()
    {
        Application.logMessageReceived -= Log;
    }

    /// <summary>
    /// Logs messages received from the console.
    /// </summary>
    /// <param name="logString">The message to be logged.</param>
    /// <param name="stackTrace">The stack trace for the log message.</param>
    /// <param name="type">The type of log message.</param>
    public void Log(string logString, string stackTrace, LogType type)
    {
        lines.Enqueue(logString + "\n");
        text.text = string.Join("", lines.ToArray());
        while (lines.Count > lineCount)
        {
            lines.Dequeue();
        }
    }
}
