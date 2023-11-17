using Engine.Systems.FSM;
using Engine.FlatPhysics;
using System.Numerics;
namespace Engine.Systems;

public abstract class Entity
{
    public FlatBody body { get; protected set; }
    public abstract void Initialize();
    public abstract void Update();
    public abstract void DrawUpdate();
    public abstract void LateUpdate();
    public abstract void Destroy();
}