using Engine.FlatPhysics;
namespace Engine.Systems;

public abstract class Entity
{
    public FlatBody body;
    protected Animator animator;

    public abstract void Initialize();
    public abstract void Update();
    public abstract void DrawUpdate();
    public abstract void LateUpdate();
    public abstract void Destroy();
}