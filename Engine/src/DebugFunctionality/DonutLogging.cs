namespace Engine.Logging;

using Engine.Data;
using Raylib_cs;
using System;
using System.Runtime.InteropServices;
public unsafe static class DonutLogging
{
    private static bool loggingSet = false;
    static DonutLogging()
    {
        logs = new List<string>();
        startTime = DateTime.Now.ToString();
    }
    public static List<string> logs;
    static string startTime;
    public static void SetLogging()
    {
        if(!loggingSet)
        {
            Raylib.SetTraceLogLevel(Raylib_cs.TraceLogLevel.LOG_DEBUG);
            Raylib.SetTraceLogCallback(&CustomLogging.LogCustom);
            //Custom Logging
            loggingSet = true;
        }
    }
    public static void WriteLog(string logString)
    {
        using(StreamWriter streamWriter = new(Paths.app+"Log"+startTime+".log", true))
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
        string message = Raylib_cs.Logging.GetLogMessage(new IntPtr(text), new IntPtr(args));
        DonutLogging.logs.Add(message);

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
        if(Settings.cVars.WriteTraceLog)
        {
            DonutLogging.WriteLog("Log: "+message);
        }
        Console.ResetColor();
    }
}