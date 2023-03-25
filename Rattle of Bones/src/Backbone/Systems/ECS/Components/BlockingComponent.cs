namespace DonutEngine.Backbone;
using Box2DX.Dynamics;
using Box2DX.Collision;
using static DonutEngine.Backbone.Systems.DonutSystems;
public class BlockingComponent : StaticComponent
{
    public float Width { get; set; }
    public float Height { get; set; }
    public Body? currentBody { get; set; }

    PolygonDef polyDef = new();

    private EntityPhysics? entityPhysics;
    public override void OnAddedToEntity(Entity entity)
    {
        
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        
    }

    public override void OnAddedToEntity(StaticEntity entity)
    {
        currentBody = physicsSystem.CreateStaticBody(this, entity);
        polyDef.SetAsBox(Width, Height);
        currentBody.CreateFixture(polyDef);
    }

    public override void OnRemovedFromEntity(StaticEntity entity)
    {
        physicsSystem.DestroyBody(currentBody);
    }
}
