namespace DonutEngine.Backbone;
using DonutEngine.Backbone;
using Raylib_cs;
using System.Numerics;

public class PositionComponent : Component
{

    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public Vector2 Scale { get; set; }
    public override void OnAddedToEntity(Entity entity)
    {
        entity.entityPos = this;
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        entity.entityPos = null;
    }
}