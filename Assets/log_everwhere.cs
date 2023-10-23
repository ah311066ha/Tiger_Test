using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class log_everwhere : MonoBehaviour
{
    // Start is called before the first frame update
    string filename = "";
    void OnEnable() { Application.logMessageReceived += Log; }
    void OnDisable() { Application.logMessageReceived -= Log; }

    public void Log(string logString, string stackTrace, LogType type)
    {
        if (filename == "")
        {
            string d = System.Environment.GetFolderPath(
              System.Environment.SpecialFolder.Desktop) + "/YOUR_LOGS";
            System.IO.Directory.CreateDirectory(d);
            filename = d + "/log.txt";
        }

        try
        {
            System.IO.File.AppendAllText(filename, logString + "\n");
        }
        catch { }
    }
}
