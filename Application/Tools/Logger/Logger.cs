using System;
using System.IO;

namespace Application.Tools.Logger;

public partial class Logger
{
    public void Log(LogLevel level, string message)
    {
        if (FileName is null)
            throw new LoggerException("Logger is not initialized");
        
        var text = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level.ToString().ToUpper()}]:\t\t{message}";
        
        if (File.Exists(FileName))
            File.AppendAllText(FileName, text + Environment.NewLine);
        else
            File.WriteAllText(FileName, text + Environment.NewLine);
    }
    public void Log(LogLevel level, Exception message)
    {
        if (FileName is null)
            throw new LoggerException("Logger is not initialized");
        
        var text = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level.ToString().ToUpper()}]:\t\t{message.Message}";
        
        if (File.Exists(FileName))
            File.AppendAllText(FileName, text + Environment.NewLine);
        else
            File.WriteAllText(FileName, text + Environment.NewLine);
    }
}