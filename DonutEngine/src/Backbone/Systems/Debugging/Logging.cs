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
            TraceLogLevel.LOG_DEBUG => ConsoleColor.Magenta,
            TraceLogLevel.LOG_INFO => ConsoleColor.Cyan,
            TraceLogLevel.LOG_WARNING => ConsoleColor.DarkYellow,
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