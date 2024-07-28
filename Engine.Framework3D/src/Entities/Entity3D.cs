using Engine.Systems.FSM;
using System.Numerics;
using Newtonsoft.Json;
using Engine.Entities;
using Raylib_CSharp.Transformations;
namespace Engine.Framework3D.Entities;

public abstract class Entity3D : IEntity
{
    protected Entity3D()
    {
        _entityID = Guid.NewGuid();
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