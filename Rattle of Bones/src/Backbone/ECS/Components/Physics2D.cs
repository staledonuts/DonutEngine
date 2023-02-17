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
        body = new();
        body.type = BodyType.Dynamic;
        body.position = Vector2.Zero;
        body.enabled = true;
        body.awake = true;
        body.allowSleep = true;
        rigidbody2D = SceneManager.GetRef().AddBodyToWorld(body);
    }

    BodyDef body;
    public Body? rigidbody2D;

    

    public override void Update(float deltaTime)
    {
        base.Update(deltaTime);
        
    }
}
