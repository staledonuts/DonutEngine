using Raylib_cs;

namespace Engine.Systems.FSM;

/// <summary>
/// <see cref="Exception"/> thrown when a <see cref="State{T}"/> with the specified key does not exist in the specified <see cref="FiniteStateMachine{T}"/>.
/// </summary>
public sealed class MissingStateException : Exception
{
    public MissingStateException() 
    {
        Raylib.TraceLog(TraceLogLevel.LOG_ERROR, "Animation State missing");
    }
    public MissingStateException(string message) : base(message) 
    {
        Raylib.TraceLog(TraceLogLevel.LOG_ERROR, message);
    }
    public MissingStateException(string message, Exception inner) : base(message, inner) 
    {
        Raylib.TraceLog(TraceLogLevel.LOG_ERROR, message);
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, inner.Message);
    }
}