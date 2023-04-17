namespace DonutEngine.Backbone.Systems.Physics;
#if DEBUG
using DonutEngine.Backbone.Systems.Debug;
#endif
using Box2DX.Common;
using Box2DX.Dynamics;
using Box2DX.Collision;
using System.Numerics;
using Raylib_cs;

public class PhysicsSystem : SystemsClass
{
    #if DEBUG
    public PhysicsInfo physicsInfo = new();
    #endif
    private static AABB worldAABB = new();
    private World? world = null;
    private Vec2 gravity = new(0, 0);
    private float timeStep = 1f / 60;
    private int velocityIterations = 8;
    private int positionIterations = 4;

    public Body CreateBody(BodyDef bodydef)
    {
        return world.CreateBody(bodydef);
    }

    public void SetSomething(Body body)
    {
        
    }

    public void DestroyBody(Body body)
    {
        world.DestroyBody(body);
    }

    public override void Start()
    {
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ Setting up Physics System ]------");
        worldAABB.LowerBound.Set(-1000.0f, -1000.0f);
		worldAABB.UpperBound.Set(1000.0f, 1000.0f);
        worldAABB.Center.SetZero();
        world = new World(worldAABB,gravity, true);
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ Physics System Initialized ]------");
    }

    public override void Update()
    {
        world.Step(timeStep, velocityIterations, positionIterations);
        #if DEBUG
        physicsInfo.currentPhysicsBodies = world.GetBodyCount();
        #endif
    }

    public override void DrawUpdate()
    {        
        
    }

    public override void LateUpdate()
    {

    }

    public override void Shutdown()
    {
        world.Dispose();
    }
}