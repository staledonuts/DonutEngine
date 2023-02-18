namespace DonutEngine.Backbone;
using System.Numerics;
using DonutEngine.Backbone.Systems;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Collision;
using Raylib_cs;

public abstract partial class Entity
{
    public class Physics2D
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
            //Get physicsworld Reference and add body.
            rigidbody2D = SceneManager.GetRef().AddBodyToWorld(body);
        }

        BodyDef body;
        public Body? rigidbody2D;

    }


}
