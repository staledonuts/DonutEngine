using Engine.Logging;

namespace Engine.Exceptions;
public sealed class NoFilesException : Exception
{
    public NoFilesException() 
    { 
        DonutLogging.Log(Raylib_cs.TraceLogLevel.LOG_ERROR, "Files not found");
    }
    public NoFilesException(string message) : base(message) 
    {
        DonutLogging.Log(Raylib_cs.TraceLogLevel.LOG_ERROR, "Files not found in: "+message);
    }
    public NoFilesException(string message, Exception inner) : base(message, inner) 
    {
        DonutLogging.Log(Raylib_cs.TraceLogLevel.LOG_ERROR, "Files not found in: "+message);
    }
}