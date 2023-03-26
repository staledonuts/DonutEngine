using Raylib_cs;
using Box2DX.Dynamics;
namespace DonutEngine.Backbone;

public class TileComponent : Component
{
    public float Width { get; set; }
    public float Height { get; set; }
    public Texture2D Texture { get; set; }
    private Body? currentBody = null;
    PolygonDef polyDef = new();

    public override void OnAddedToEntity(StaticEntity entity)
    {
        currentBody = entity.entityPhysics.currentBody;
        polyDef.SetAsBox(Width, Height);
        currentBody.CreateFixture(polyDef);        
    }

    public override void OnRemovedFromEntity(StaticEntity entity)
    {
        
    }

    public override void OnAddedToEntity(DynamicEntity entity)
    {
        //throw new NotImplementedException();
    }

    public override void OnRemovedFromEntity(DynamicEntity entity)
    {
        //throw new NotImplementedException();
    }
}