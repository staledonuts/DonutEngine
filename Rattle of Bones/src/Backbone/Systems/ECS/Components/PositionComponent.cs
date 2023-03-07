namespace DonutEngine.Backbone;
using DonutEngine.Backbone;
using System.Numerics;

public class PositionComponent : Component
{

    public Vector2 Vector { get; set; }
    
    public Vector2 GetPosition()
    {
        return Vector;
    }

    public void SetPosition(Vector2 vector2)
    {
        Vector = vector2;
    }

    public override void OnAddedToEntity(Entity entity)
    {
        entity.entityPos = this;
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        entity.entityPos = null;
    }
}