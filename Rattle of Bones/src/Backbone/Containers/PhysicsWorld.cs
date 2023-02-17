namespace DonutEngine.Backbone.Systems;
using System.Numerics;
using Box2D.NetStandard.Common;
using Box2D.NetStandard.Dynamics.World;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Collision;

public class PhysicsWorld
{
    Vector2 setGravity = new(0, -10);
    public World? b2world;

    
    const float timeStep = 1f / 60f;
    const int velocityIterations = 7;
    const int positionIterations = 4;

    public void InitializePhysicsWorld()
    {
        b2world = new World(setGravity);
    }

    public Body AddBodyToWorld(BodyDef body)
    {
        Body returnBody = b2world.CreateBody(body);
        returnBody.SetLinearVelocity(new(10,10));
        return returnBody;
    }

    public void UpdateStep()
    {
        b2world.Step(timeStep, velocityIterations, positionIterations);
    }

}
