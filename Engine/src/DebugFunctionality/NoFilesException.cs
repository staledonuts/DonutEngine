using Raylib_CSharp.Logging;

namespace Engine.Exceptions;
public sealed class NoFilesException : Exception
{
    public NoFilesException() 
    { 
        Logger.TraceLog(TraceLogLevel.Error, "Files not found");
    }
    public NoFilesException(string message) : base(message) 
    {
        Logger.TraceLog(TraceLogLevel.Error, "Files not found in: "+message);
    }
    public NoFilesException(string message, Exception inner) : base(message, inner) 
    {
        Logger.TraceLog(TraceLogLevel.Error, "Files not found in: "+message);
    }
}