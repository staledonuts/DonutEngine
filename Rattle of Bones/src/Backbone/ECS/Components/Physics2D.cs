namespace DonutEngine.Backbone;
using static DonutEngine.Backbone.ECS;
using System.Numerics;
using DonutEngine.Backbone.Systems;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Collision;
using Raylib_cs;

public class Physics2D : Component
{
    public Physics2D(Vector2 position)
    {
        body = new()
        {
            type = BodyType.Dynamic,
            awake = true,
            position = Vector2.Zero,
            gravityScale = -9,
            enabled = true,
            allowSleep = true
        };
        rigidbody2D = SceneManager.GetRef().AddBodyToWorld(body);
    }

    BodyDef body;
    public Body? rigidbody2D;

    

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
    }
}
