using Engine.Logging;

namespace Engine.Exceptions;
public sealed class MissingSceneException : Exception
{
    public MissingSceneException() { }
    public MissingSceneException(string message) : base(message) 
    {
        DonutLogging.Log(Raylib_cs.TraceLogLevel.LOG_ERROR, message);
    }
    public MissingSceneException(string message, Exception inner) : base(message, inner) 
    {
        DonutLogging.Log(Raylib_cs.TraceLogLevel.LOG_ERROR, message);
    }
}