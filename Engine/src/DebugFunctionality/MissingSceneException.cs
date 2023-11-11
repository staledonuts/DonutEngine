using Raylib_cs;

namespace Engine.Exceptions;
public sealed class MissingSceneException : Exception
{
    public MissingSceneException() { }
    public MissingSceneException(string message) : base(message) 
    {
        Raylib.TraceLog(Raylib_cs.TraceLogLevel.LOG_ERROR, message);
    }
    public MissingSceneException(string message, Exception inner) : base(message, inner) 
    {
        Raylib.TraceLog(Raylib_cs.TraceLogLevel.LOG_ERROR, message);
    }
}