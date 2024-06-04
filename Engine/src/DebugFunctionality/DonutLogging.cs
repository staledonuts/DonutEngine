namespace Engine.Logging;
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
            Raylib.SetTraceLogLevel(Raylib_cs.TraceLogLevel.Debug);
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
            TraceLogLevel.All => ConsoleColor.White,
            TraceLogLevel.Trace => ConsoleColor.Black,
            TraceLogLevel.Debug => ConsoleColor.White,
            TraceLogLevel.Info => ConsoleColor.Cyan,
            TraceLogLevel.Warning => ConsoleColor.DarkYellow,
            TraceLogLevel.Error => ConsoleColor.Black,
            TraceLogLevel.Fatal => ConsoleColor.Black,
            TraceLogLevel.None => ConsoleColor.Black,
            _ => throw new ArgumentOutOfRangeException(nameof(logLevel), logLevel, null)
        };
        Console.BackgroundColor = (TraceLogLevel)logLevel switch
        {
            TraceLogLevel.All => ConsoleColor.Black,
            TraceLogLevel.Trace => ConsoleColor.White,
            TraceLogLevel.Debug => ConsoleColor.DarkGreen,
            TraceLogLevel.Info => ConsoleColor.Black,
            TraceLogLevel.Warning => ConsoleColor.Black,
            TraceLogLevel.Error => ConsoleColor.Red,
            TraceLogLevel.Fatal => ConsoleColor.Red,
            TraceLogLevel.None => ConsoleColor.White,
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