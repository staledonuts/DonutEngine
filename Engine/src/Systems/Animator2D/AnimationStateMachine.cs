using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Raylib_CSharp.Transformations;

namespace Engine.Systems.ASM;

public sealed class AnimationStateMachine
{
    readonly Animator _animator = new();
    [AllowNull] AnimationState _current;
    
    private Dictionary<Entity, AnimationState> _entityStates = new Dictionary<Entity, AnimationState>();

    public void AddState(Entity entity, AnimationState initialState)
    {
        if (!_entityStates.ContainsKey(entity))
        {
            _entityStates[entity] = initialState;
            initialState.Enter(_animator);
        }
    }

    public void ChangeState(Entity entity, AnimationState newState)
    {
        if (_entityStates.ContainsKey(entity))
        {
            _entityStates[entity].Exit();
            _entityStates[entity] = newState;
            newState.Enter(_animator);
        }
    }

    /// <summary>
    /// Updates the current <see cref="State{T}"/> in the <see cref="FiniteStateMachine{T}"/>.
    /// </summary>
    /// <param name="deltaTime">The time since the last frame.</param>
    public void Update() => _current.Update(_animator);

    public void DrawUpdate() => _current.DrawUpdate(_animator);

    public void LateUpdate() => _current.LateUpdate(_animator);
}