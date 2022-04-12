using System.Globalization;
using System.Text;

namespace osuToolsV2.Logger;

public class FileLogger
{
    public FileLogger(string? logFile = null, FileMode openMode = FileMode.Truncate)
    {
        if (!string.IsNullOrEmpty(logFile))
        {
            LogFile = logFile ?? "log.txt";
        }
        OpenMode = openMode;
    }
    public string LogFile { get; set; } = "log.txt";
    public FileMode OpenMode { get; set; }

    public void Log(string level, string module, string content, bool logTime = true, bool newLine = true)
    {
        string time = logTime
            ? DateTime.Now.ToString(CultureInfo.CurrentCulture)
            : "";
        StringBuilder builder = new StringBuilder();
        builder.Append(time);
        builder.Append($"[{module}]");
        builder.Append($" {level} :");
        builder.Append(content);
        if (newLine)
        {
            builder.AppendLine();
        }
    }

    public void LogError(string module, string content, bool logTime = true, bool newLine = true) =>
        Log("Error", module, content, logTime, newLine);
    
    public void LogException(string module, string content, bool logTime = true, bool newLine = true) =>
        Log("Exception", module, content, logTime, newLine);
    
    public void LogWarning(string module, string content, bool logTime = true, bool newLine = true) =>
        Log("Warning", module, content, logTime, newLine);
    
    public void LogDebug(string module, string content, bool logTime = true, bool newLine = true) =>
        Log("Debug", module, content, logTime, newLine);
    
    public void LogInfo(string module, string content, bool logTime = true, bool newLine = true) =>
        Log("Info", module, content, logTime, newLine);

    public static FileLogger GlobalFileLogger
    {
        get
        {
            DateTime now = DateTime.Now;
            string fileName = $"{now.Year}/{now.Month}/{now.Day}-{now.Hour}-{now.Minute}-{now.Second}";
            return new FileLogger(fileName);
        }
    }
}