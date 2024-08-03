using Engine.Entities;
using Engine.Framework2D.FlatPhysics;
using Raylib_CSharp;
namespace Engine.Framework2D.Entities;


public class EntitiesData2D : IEntitiesData
{
    public EntitiesData2D()
    {
        _entities = new();
        world = new();
        EntityID = 0;
    }

    public int EntityID;

    const int iterations = 8;
    public readonly FlatWorld world;
    private Dictionary<Guid, IEntity> _entities;

    Dictionary<Guid, IEntity> IEntitiesData.Entities 
    { 
        get 
        {
            return _entities;
        }
    }

    public void UpdatePhysics()
    {
        world.Step(Time.GetFrameTime(), iterations);
    }

    public void CreateEntity(Entity2D entity)
    {
        _entities.Add(entity.GetGuid(), entity);
        world.AddBody(entity.Body);
        entity.Initialize();
    }
    
}