namespace Engine.Systems.ECS;
using Raylib_cs;
using System.Numerics;

public class Physics2DComponent : Component
{
    public Vector2 Velocity { get; set; }
    public Vector2 Acceleration { get; set; }

    public override void OnComponentAdded() 
    {
        
    }
    public override void OnComponentRemoved() 
    {
        
    }

}
