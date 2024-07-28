
using Raylib_CSharp;

namespace Engine.Entities;

public interface IEntitiesData
{
    public Dictionary<Guid, IEntity> Entities
    {
        get;
    }
    const int iterations = 8;
    public void UpdatePhysics();
}
