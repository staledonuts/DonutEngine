using Raylib_CSharp;

namespace Engine.Entities;

public class DummyEntitiesData : IEntitiesData
{

    const int iterations = 8;
    
    private Dictionary<Guid, IEntity> _entities;

    public DummyEntitiesData()
    {
        _entities = new();
    }

    public Dictionary<Guid, IEntity> Entities
    {
        get
        {
            return _entities;
        }
    }

    public void UpdatePhysics()
    {

    }
}
