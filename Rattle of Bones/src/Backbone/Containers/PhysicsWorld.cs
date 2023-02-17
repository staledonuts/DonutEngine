namespace DonutEngine.Backbone.Systems;
using System.Numerics;
using Box2D.NetStandard.Common;
using Box2D.NetStandard.Dynamics.World;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Collision;

public class PhysicsWorld
{
    bool canSleep = true;
    Vector2 gravity = new(0, -10);
    public World? b2world;
    float timeStep = 1f / 60f;
    int velocityIterations = 7;
    int positionIterations = 4;

    public void InitializePhysicsWorld()
    {
        b2world = new World(gravity);
    }

    public Body AddBodyToWorld(BodyDef body)
    {
        Body returnBody = b2world.CreateBody(body);
        return returnBody;
    }

    public void UpdateStep()
    {
        b2world.Step(timeStep, velocityIterations, positionIterations);
    }

}
