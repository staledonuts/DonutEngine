namespace DonutEngine.Backbone;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Collision.Shapes;
using static DonutEngine.Backbone.Systems.DonutSystems;
public class BlockingComponent : Component
{
    public float Width { get; set; }
    public float Height { get; set; }
    public Body? Body { get; set; }

    PolygonShape polygonShape = new();

    private PositionComponent? position;
    public override void OnAddedToEntity(Entity entity)
    {
        position = entity.entityPos;
        Body = physicsSystem.CreateStaticBody(this, entity);
        polygonShape.SetAsBox(Width, Height);
        Body.CreateFixture(polygonShape);
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        physicsSystem.DestroyBody(Body);
    }
}
