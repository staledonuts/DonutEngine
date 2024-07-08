using Raylib_CSharp.Logging;

namespace Engine.Exceptions;
public sealed class MissingSceneException : Exception
{
    public MissingSceneException() { }
    public MissingSceneException(string message) : base(message) 
    {
        Logger.TraceLog(TraceLogLevel.Error, message);
    }
    public MissingSceneException(string message, Exception inner) : base(message, inner) 
    {
        Logger.TraceLog(TraceLogLevel.Error, message);
    }
}