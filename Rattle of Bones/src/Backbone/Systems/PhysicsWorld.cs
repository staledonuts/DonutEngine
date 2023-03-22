namespace DonutEngine.Backbone.Systems;
using System.Numerics;
using Box2D.NetStandard.Common;
using Box2D.NetStandard.Dynamics.World;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Collision.Shapes;
using Box2D.NetStandard.Dynamics.Fixtures;
using Box2D.NetStandard.Dynamics.Contacts;
using Raylib_cs;

public class PhysicsSystem : SystemsClass
{
    private World world = new World(new Vector2(0, 200));
    private float timeStep = 1f / 60f;
    private int velocityIterations = 9;
    private int positionIterations = 4;

    public Body CreateBody(PhysicsComponent physics, Entity entity) 
    {
        BodyDef bodyDef = new BodyDef();
        bodyDef.type = physics.Type;
        bodyDef.position = entity.entityPos.Position;
        Body body = world.CreateBody(bodyDef);

        FixtureDef fixtureDef = new FixtureDef();
        fixtureDef.shape = new PolygonShape();
        ((PolygonShape)fixtureDef.shape).SetAsBox(physics.Width / 2f, physics.Height / 2f);
        fixtureDef.density = physics.Density;
        fixtureDef.friction = physics.Friction;
        fixtureDef.restitution = physics.Restitution;

        body.CreateFixture(fixtureDef);

        return body;
    }

    public Body CreateStaticBody(BlockingComponent blockingComponent, Entity entity) 
    {
        BodyDef bodyDef = new BodyDef();
        bodyDef.type = BodyType.Static;
        bodyDef.position = entity.entityPos.Position;
        Body body = world.CreateBody(bodyDef);

        return body;
    }

    public void DestroyBody(Body body)
    {
        world.DestroyBody(body);
    }

    public override void Start()
    {
        //throw new NotImplementedException();
    }

    public override void Update()
    {
        world.Step(timeStep, velocityIterations, positionIterations);
    }

    public override void DrawUpdate()
    {

    }

    public override void LateUpdate()
    {
        //throw new NotImplementedException();
    }

    public override void Shutdown()
    {
        //throw new NotImplementedException();
    }
}
