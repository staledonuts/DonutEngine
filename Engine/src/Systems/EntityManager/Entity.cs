using Engine.Systems.FSM;
using Engine.FlatPhysics;
using System.Numerics;
namespace Engine.Systems;

public abstract class Entity
{
    protected Entity(Vector2 position, float density, float mass, float restitution, float area, bool isStatic, float radius, float width, float height, ShapeType shapeType)
    {
        body = new(position, density, mass, restitution, area, isStatic, radius, width, height, shapeType);
    }

    protected Entity(FlatBody flatBody)
    {
        body = flatBody;
    }

    public FlatBody body { get; protected set; }
    public Vector2 Position 
    {
        get
        {
            return body.Position;
        }
    }
    public abstract void Initialize();
    public abstract void Update();
    public abstract void DrawUpdate();
    public abstract void LateUpdate();
    public abstract void Destroy();
}