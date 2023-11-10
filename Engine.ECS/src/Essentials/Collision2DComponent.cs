using System.Numerics;
using Raylib_cs;

namespace Engine.Systems.ECS;

public class Collision2DComponent : Component
{
    public Collision2DComponent(float radius)
    {
        Radius = radius;
    }

    public float Radius { get; set; }

    public override void OnComponentAdded()
    {
        
    }

    public override void OnComponentRemoved()
    {
        
    }
}