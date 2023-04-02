namespace DonutEngine.Backbone;
using Raylib_cs;

public class ColorBoxComponent: Component
{
    public int width { get; set; }
    public int height { get; set; }
    public Color color { get; set; }

    public override void OnAddedToEntity(Entity entity)
    {
        
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        
    }
}
