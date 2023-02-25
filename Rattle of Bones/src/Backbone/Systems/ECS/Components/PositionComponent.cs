using DonutEngine.Backbone;
using System.Numerics;

public class PositionComponent : Component
{
    public float X { get; set; }
    public float Y { get; set; }

    private Vector2 vector = new(0,0);
    
    public Vector2 GetPosition()
    {
        vector.X = X;
        vector.Y = Y;
        return vector;
    }

    public void SetPosition(Vector2 vector2)
    {
        vector = vector2;
    }

    public override void OnAddedToEntity(Entity entity)
    {
        
    }

    public override void OnRemovedFromEntity(Entity entity)
    {
        
    }
}