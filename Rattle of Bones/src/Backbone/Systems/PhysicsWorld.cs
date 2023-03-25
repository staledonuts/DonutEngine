namespace DonutEngine.Backbone.Systems;
using System.Numerics;
using Box2DX.Common;
using static Box2DX.Dynamics.World;
using Box2DX.Dynamics;
using Box2DX.Collision;
using Raylib_cs;

public class PhysicsSystem : SystemsClass
{
    private static AABB worldAABB = new();
    private World? world;

    private Vec2 gravity = new(0, 50);
    private float timeStep = 1f / DonutSystems.settingsVars.targetFPS;
    private int velocityIterations = 9;
    private int positionIterations = 4;

    public Body CreateBody(EntityPhysics physics, Entity entity) 
    {
        BodyDef bodyDef = new BodyDef();
        bodyDef.Position.Set(physics.X, physics.Y);
        Body body = world.CreateBody(bodyDef);
        PolygonDef polygonDef = new PolygonDef();
        polygonDef.SetAsBox(physics.Width / 2f, physics.Height / 2f);
        polygonDef.Density = physics.Density;
        polygonDef.Friction = physics.Friction;
        polygonDef.Restitution = physics.Restitution;
        body.CreateFixture(polygonDef);
        body.SetMassFromShapes();

        return body;
    }

    public Body CreateStaticBody(BlockingComponent blockingComponent, Entity entity) 
    {
        BodyDef bodyDef = new BodyDef();
        bodyDef.Position.Set(entity.entityPhysics.X, entity.entityPhysics.Y);
        Body body = world.CreateBody(bodyDef);
        body.SetStatic();

        return body;
    }

    public void DestroyBody(Body body)
    {
        world.DestroyBody(body);
    }

    public override void Start()
    {
        worldAABB.LowerBound.Set(-100.0f, -100.0f);
		worldAABB.UpperBound.Set(100.0f, 100.0f);
        world = new World(worldAABB,gravity, true);
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
