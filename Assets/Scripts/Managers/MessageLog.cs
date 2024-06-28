using UnityEngine;

[System.Serializable]
public class MessageLog
{
    public static void LogMessage(string text)
    {
        Debug.ClearDeveloperConsole();
        Debug.Log(text);
    }
}
