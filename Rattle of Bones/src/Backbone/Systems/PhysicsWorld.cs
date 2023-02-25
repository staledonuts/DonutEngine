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
    private static PhysicsSystem? instance = null;

    public static PhysicsSystem Instance 
    {
        get 
        {
            if (instance == null) 
            {
                instance = new PhysicsSystem();
            }
            return instance;
        }
    }
    private World world = new World(new Vector2(0, -10));
    private float timeStep = 1f / 60f;
    private int velocityIterations = 8;
    private int positionIterations = 3;
        

    /*foreach (Entity entity in Program.entityManager.EntitiesWith<PhysicsComponent>()) 
    {
        PhysicsComponent physics = entity.GetComponent<PhysicsComponent>();
        PositionComponent position = entity.GetComponent<PositionComponent>();

        position.X = physics.Body.Position.X;
        position.Y = physics.Body.Position.Y;
    }*/

    public Body CreateBody(PhysicsComponent physics) 
    {
        BodyDef bodyDef = new BodyDef();
        bodyDef.type = physics.Type;
        bodyDef.position = new Vector2(physics.X, physics.Y);

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
