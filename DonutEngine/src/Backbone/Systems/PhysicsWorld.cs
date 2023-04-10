namespace DonutEngine.Backbone.Systems;
using Box2DX.Common;
using Box2DX.Dynamics;
using Box2DX.Collision;
using Raylib_cs;

public class PhysicsSystem : SystemsClass
{
    private static AABB worldAABB = new();
    private World? world = null;

    private Vec2 gravity = new(0, 10);
    private float timeStep = 1f / DonutSystems.settingsVars.targetFPS;
    private int velocityIterations = 8;
    private int positionIterations = 4;

    public Body CreateBody(float X, float Y, float Width, float Height, float Density, float Friction, float Restitution) 
    {
        BodyDef bodyDef = new BodyDef();
        bodyDef.Position.Set(X, Y);
        Body body = world.CreateBody(bodyDef);
        PolygonDef polygonDef = new PolygonDef();
        polygonDef.SetAsBox(Width / 2f, Height / 2f);
        polygonDef.Density = Density;
        polygonDef.Friction = Friction;
        polygonDef.Restitution = Restitution;
        body.CreateFixture(polygonDef);
        body.SetMassFromShapes();
        return body;
    }

    public Body CreateStaticBody(float X, float Y) 
    {
        BodyDef bodyDef = new BodyDef();
        bodyDef.Position.Set(X, Y);
        Body body = world.CreateBody(bodyDef);
        body.IsStatic();

        return body;
    }

    public void DestroyBody(Body body)
    {
        world.DestroyBody(body);
    }

    public override void Start()
    {
        worldAABB.LowerBound.Set(-1000.0f, -1000.0f);
		worldAABB.UpperBound.Set(1000.0f, 1000.0f);
        world = new World(worldAABB,gravity, true);
    }

    public override void Update()
    {
        world.Step(timeStep, velocityIterations, positionIterations);
    }

    public override void DrawUpdate()
    {        
        Raylib.DrawText("Number of Bodies:"+world.GetBodyCount(),0,50, 12, Raylib_cs.Color.BLACK);
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