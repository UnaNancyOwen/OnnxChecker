using System;
using UnityEngine;

public class CustomLogHandler : ILogHandler
{
    private readonly ILogHandler default_handler;

    public CustomLogHandler(ILogHandler default_handler)
    {
        this.default_handler = default_handler;
    }

    public void LogFormat(LogType type, UnityEngine.Object context, string format, params object[] args)
    {
        if (type != LogType.Warning && type != LogType.Error)
        {
            default_handler.LogFormat(type, context, $"[{type}] {format}", args);
            return;
        }

        if (type == LogType.Warning) { Console.ForegroundColor = ConsoleColor.Yellow; }
        if (type == LogType.Error) { Console.ForegroundColor = ConsoleColor.Red; }
        Console.WriteLine($"[{type}] {String.Format(format, args)}");
        Console.ResetColor();
    }

    public void LogException(Exception exception, UnityEngine.Object context)
    {
        default_handler.LogException(exception, context);
    }
}
