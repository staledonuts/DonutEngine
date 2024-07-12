using Raylib_CSharp.Logging;

namespace Engine.Systems.ASM;

public sealed class MissingAnimationStateException : Exception
{
    public MissingAnimationStateException() 
    {
        Logger.TraceLog(TraceLogLevel.Error, "Animation State missing");
    }
    public MissingAnimationStateException(string message) : base(message) 
    {
        Logger.TraceLog(TraceLogLevel.Error, message);
    }
    public MissingAnimationStateException(string message, Exception inner) : base(message, inner) 
    {
        Logger.TraceLog(TraceLogLevel.Error, message);
        Logger.TraceLog(TraceLogLevel.Info, inner.Message);
    }
}