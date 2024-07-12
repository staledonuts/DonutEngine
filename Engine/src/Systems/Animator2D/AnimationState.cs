using System.Diagnostics.CodeAnalysis;
using Newtonsoft.Json;
using Raylib_CSharp.Logging;
using Raylib_CSharp.Transformations;

namespace Engine.Systems.ASM;

public abstract class AnimationState
{
    [AllowNull] public AnimationData animationData
    {
        get;
        protected set;
    }
    [AllowNull] protected Entity _entity;
    protected Rectangle _targetRect = new();
    public abstract void Init(Entity entity);
    public abstract void Enter(AnimationStateMachine animator);
    public abstract void Update(AnimationStateMachine animator);
    public abstract void DrawUpdate(AnimationStateMachine animator);
    public abstract void LateUpdate(AnimationStateMachine animator);
    public abstract void Exit();

    public string GetAnimationName()
    {
        return animationData.Name;
    }

    #if DEBUG
    public void SerializeAnimation()
    {
        Logger.TraceLog(TraceLogLevel.Debug, "Serializing to "+Paths.AnimationsPath+"typeName_animationData.Name_animation.json");
        string jsonString = JsonConvert.SerializeObject(animationData, Formatting.Indented);
        string typeName = _entity.GetType().Name;
        File.WriteAllText(Paths.AnimationsPath+$"{typeName}_{animationData.Name}_animation.json", jsonString);
    }
    #endif
}

