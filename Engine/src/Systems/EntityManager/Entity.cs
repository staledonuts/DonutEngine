using Engine.Systems.FSM;
using Engine.FlatPhysics;
using System.Numerics;
using Newtonsoft.Json;
using Raylib_CSharp.Transformations;
namespace Engine.Systems;

public abstract class Entity : IDisposable
{
    protected Entity(Vector2 position, float density, float mass, float restitution, float area, bool isStatic, float radius, float width, float height, ShapeType shapeType)
    {
        body = new(position, density, mass, restitution, area, isStatic, radius, width, height, shapeType);
        _guid = Guid.NewGuid();
    }

    protected Entity(FlatBody flatBody)
    {
        body = flatBody;
        _guid = Guid.NewGuid();
    }

    private Guid _guid;

    public Guid EntityID
    {
        get
        {
            return _guid;
        }
    }



    [JsonProperty] public FlatBody body { get; protected set; }
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

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}