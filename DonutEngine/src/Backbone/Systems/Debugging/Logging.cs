namespace DonutEngine.Backbone.Systems.Debug;
using Raylib_cs;
using static Raylib_cs.Color;
using System;
using System.Runtime.InteropServices;

public static class Logging
{
    public static void WriteLog(string logString)
    {
        
        using(StreamWriter streamWriter = new(AppDomain.CurrentDomain.BaseDirectory+"Log.log", true))
        {
            streamWriter.WriteLine(logString);
        }
    }

    
}

public unsafe class CustomLogging
{
    [UnmanagedCallersOnly(CallConvs = new[] { typeof(System.Runtime.CompilerServices.CallConvCdecl) })]
    public static void LogCustom(int logLevel, sbyte* text, sbyte* args)
    {
        var message = Raylib_cs.Logging.GetLogMessage(new IntPtr(text), new IntPtr(args));

        Console.ForegroundColor = (TraceLogLevel)logLevel switch
        {
            TraceLogLevel.LOG_ALL => ConsoleColor.White,
            TraceLogLevel.LOG_TRACE => ConsoleColor.Black,
            TraceLogLevel.LOG_DEBUG => ConsoleColor.White,
            TraceLogLevel.LOG_INFO => ConsoleColor.Cyan,
            TraceLogLevel.LOG_WARNING => ConsoleColor.DarkYellow,
            TraceLogLevel.LOG_ERROR => ConsoleColor.Black,
            TraceLogLevel.LOG_FATAL => ConsoleColor.Black,
            TraceLogLevel.LOG_NONE => ConsoleColor.Black,
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
        };
        Console.BackgroundColor = (TraceLogLevel)logLevel switch
        {
            TraceLogLevel.LOG_ALL => ConsoleColor.Black,
            TraceLogLevel.LOG_TRACE => ConsoleColor.White,
            TraceLogLevel.LOG_DEBUG => ConsoleColor.DarkGreen,
            TraceLogLevel.LOG_INFO => ConsoleColor.Black,
            TraceLogLevel.LOG_WARNING => ConsoleColor.Black,
            TraceLogLevel.LOG_ERROR => ConsoleColor.Red,
            TraceLogLevel.LOG_FATAL => ConsoleColor.Red,
            TraceLogLevel.LOG_NONE => ConsoleColor.White,
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
        };
        
        Console.WriteLine($"Log: " + message);
        if(DonutSystems.settingsVars.logging)
        {
            Logging.WriteLog("Log: "+message);
        }
        Console.ResetColor();
    }
}