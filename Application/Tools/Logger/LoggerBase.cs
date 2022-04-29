using System;
using System.Globalization;

namespace Application.Tools.Logger;
public partial class Logger
{
    private Logger() { }
    
    public enum LogLevel
    {
        Debug,
        Info,
        Warning,
        Error,
        Fatal
    }
    private static string? Path { get; set; }
    private static string? FileName { get; set; }

    private static Logger? _instance;
    public static Logger Instance => _instance ??= new Logger();

    public static void Init(string path)
    {
        Path = path;
        // 08-17-2000T16-32-32 - format
        FileName = @$"{Path}\{DateTime.Now.ToString("MM-dd-yyyy")}_T_{DateTime.Now.ToString("HH-mm-ss")}.log";
    }
}