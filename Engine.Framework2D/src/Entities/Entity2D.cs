using Engine.Systems.FSM;
using Engine.Framework2D.FlatPhysics;
using System.Numerics;
using Newtonsoft.Json;
using Raylib_CSharp.Transformations;
using Engine.Entities;
namespace Engine.Framework2D.Entities;

public abstract class Entity2D : IEntity
{
    protected Entity2D(Vector2 position, float density, float mass, float restitution, float area, bool isStatic, float radius, float width, float height, float sizeMultiplier, ShapeType shapeType)
    {
        Body = new(position, density, mass, restitution, area, isStatic, radius, width, height, sizeMultiplier, shapeType);
        _entityID = Guid.NewGuid();
    }

    protected Entity2D(FlatBody flatBody)
    {
        Body = flatBody;
        _entityID = Guid.NewGuid();
    }

    [JsonProperty] public FlatBody Body 
    {
        get; 
        protected set; 
    }
    public Vector2 Position 
    { 
        get
        {
            return Body.Position;
        }
        set
        {
           Body.Move(value); 
        }
    }
    public Vector2 Scale 
    { 
        get
        {
            return Body.Size;
        } 
        set 
        {
            Body.SizeMultiplier = value.X;
        }
    }
    public float Rotation 
    { 
        get 
        {
            return Body.Rotation;
        } 
        set 
        {
            Body.Rotate(value);
        }
    }

    [JsonProperty] protected Guid _entityID;

    public Guid GetGuid()
    {
        return _entityID;
    }

    public abstract void Initialize();

    public abstract void Update();

    public abstract void DrawUpdate();

    public abstract void LateUpdate();

    public abstract void Destroy();

    public abstract void Dispose();
}