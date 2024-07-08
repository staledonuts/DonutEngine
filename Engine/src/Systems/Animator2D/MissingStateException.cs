using Raylib_CSharp.Logging;

namespace Engine.Systems.FSM;

/// <summary>
/// <see cref="Exception"/> thrown when a <see cref="State{T}"/> with the specified key does not exist in the specified <see cref="FiniteStateMachine{T}"/>.
/// </summary>
public sealed class MissingStateException : Exception
{
    public MissingStateException() 
    {
        Logger.TraceLog(TraceLogLevel.Error, "Animation State missing");
    }
    public MissingStateException(string message) : base(message) 
    {
        Logger.TraceLog(TraceLogLevel.Error, message);
    }
    public MissingStateException(string message, Exception inner) : base(message, inner) 
    {
        Logger.TraceLog(TraceLogLevel.Error, message);
        Logger.TraceLog(TraceLogLevel.Info, inner.Message);
    }
}