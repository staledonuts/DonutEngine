namespace DonutEngine.Backbone;
using static DonutEngine.Backbone.ECS;
using System.Numerics;
using static DonutEngine.Backbone.PhysicsWorld;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Collision;
using Raylib_cs;

public class Physics2D : Component
{
    public Physics2D(Vector2 position)
    {
        body = new();
        body.type = BodyType.Dynamic;
        body.position = Vector2.Zero;
        position = body.position;
        body.enabled = true;
        body.awake = true;
        body.allowSleep = true;
        rigidbody2D = AddBodyToWorld(body);
    }

    BodyDef body;
    public BodyDef? rigidbody2D;
    
    public Vector2 position;

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
    }
}
