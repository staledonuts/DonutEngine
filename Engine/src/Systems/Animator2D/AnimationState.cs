using Newtonsoft.Json;

namespace Engine.Systems.FSM;

/// <summary>
/// An empty <see cref="State{T}"/>.
/// </summary>
/// <remarks>Use <see cref="State">Empty&lt;T&gt;.State </see>to get an empty <see cref="State{T}"/></remarks>
/// <inheritdoc cref="State{T}"/>
sealed class AnimationState<T> : State<T>
{
    [JsonProperty(propertyName:"Animation Data")] AnimationData _animationData;
    /// <summary>
    /// An instance of an empty <see cref="State{T}"/>.
    /// </summary>
    public static readonly AnimationState<T> State = new();

    AnimationState() { }

    /// <inheritdoc />
    void State<T>.Init(FiniteStateMachine<T> fsm) { }

    /// <inheritdoc />
    void State<T>.Enter() { }

    /// <inheritdoc />
    void State<T>.Update(float deltaTime) { }

    /// <inheritdoc />
    void State<T>.Exit() { }

    void State<T>.DrawUpdate() { }

    void State<T>.LateUpdate() { }
}