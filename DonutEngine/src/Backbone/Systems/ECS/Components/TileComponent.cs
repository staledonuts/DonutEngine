using Raylib_cs;
using Box2DX.Dynamics;
using Box2DX.Collision;
namespace DonutEngine.Backbone;

public class TileComponent : Component
{
    public float Width { get; set; }
    public float Height { get; set; }
    public Texture2D Texture { get; set; }
    private Body? currentBody = null;
    PolygonDef polyDef = new();

    public override void OnAddedToEntity(Entity entity)
    {
        /*currentBody = entity.body;
        polyDef.SetAsBox(Width, Height);
        currentBody.CreateFixture(polyDef);*/
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        Dispose();
    }
}