namespace DonutEngine.Backbone;
using System.Numerics;
using DonutEngine.Backbone.Systems;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Collision.Shapes;
using Raylib_cs;

public abstract partial class Entity
{
    public class Physics2D
    {
        public Physics2D(Vector2 position)
        {
            bodyDef = new()
            {
                type = BodyType.Dynamic,
                awake = true,
                position = position,
                enabled = true,
                allowSleep = false
            };

            circle = new();
            //Get physicsworld Reference and add body.
            rigidbody2D = SceneManager.GetRef().AddBodyToWorld(bodyDef);
            circle.Set(rigidbody2D.GetLocalCenter(), 2);
        }

        public BodyDef bodyDef;
        public Body? rigidbody2D;
        public CircleShape circle;

    }


}
