using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Raylib_CSharp.Transformations;

namespace Engine.Systems.ASM;

public abstract class AnimationState
{
    [AllowNull][JsonProperty(propertyName:"Animation Data")] protected AnimationData animationData;
    [AllowNull] protected Entity _entity;
    protected Rectangle _targetRect = new();
    public abstract void Init(Entity entity);
    public abstract void Enter(Animator animator);
    public abstract void Update(Animator animator);
    public abstract void DrawUpdate(Animator animator);
    public abstract void LateUpdate(Animator animator);
    public abstract void Exit();
}