using Raylib_cs;
using DonutEngine.Backbone;

public class TileComponent : StaticComponent
{
    public int X { get; set; }
    public int Y { get; set; }
    public Texture2D Texture { get; set; }

    public override void OnAddedToEntity(StaticEntity entity)
    {
        
    }

    public override void OnAddedToEntity(Entity entity)
    {

    }

    public override void OnRemovedFromEntity(StaticEntity entity)
    {
        
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        
    }
}