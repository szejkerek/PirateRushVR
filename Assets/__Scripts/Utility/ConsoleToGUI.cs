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
        lines.Enqueue(logString + "\n");
        text.text = string.Join("", lines.ToArray());
        while (lines.Count > lineCount)
        {
            lines.Dequeue();
        }
    }
}
