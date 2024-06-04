using Raylib_cs;

namespace Engine.Exceptions;
public sealed class NoFilesException : Exception
{
    public NoFilesException() 
    { 
        Raylib.TraceLog(Raylib_cs.TraceLogLevel.Error, "Files not found");
    }
    public NoFilesException(string message) : base(message) 
    {
        Raylib.TraceLog(Raylib_cs.TraceLogLevel.Error, "Files not found in: "+message);
    }
    public NoFilesException(string message, Exception inner) : base(message, inner) 
    {
        Raylib.TraceLog(Raylib_cs.TraceLogLevel.Error, "Files not found in: "+message);
    }
}