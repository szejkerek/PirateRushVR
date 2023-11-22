using System.Collections.Generic;
using TMPro;
using UnityEngine;

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

    public void Log(string logString, string stackTrace, LogType type)
    {
        // Enqueue the log message
        lines.Enqueue(logString + "\n");

        // Update the text by joining the lines in the queue
        text.text = string.Join("", lines.ToArray());

        // Remove oldest logs if the queue exceeds lineCount
        while (lines.Count > lineCount)
        {
            lines.Dequeue();
        }
    }
}
