using Engine.Systems.FSM;
using Engine.FlatPhysics;
namespace Engine.Systems;

public abstract class Entity
{
    public abstract void Initialize();
    public abstract void Update();
    public abstract void DrawUpdate();
    public abstract void LateUpdate();
    public abstract void Destroy();
}