namespace DonutEngine.Backbone;
using System.Numerics;
using Box2D.NetStandard.Common;
using Box2D.NetStandard.Dynamics.World;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Collision;

public static class PhysicsWorld
{
    static bool canSleep = true;
    static Vector2 gravity = new(0, -10);
    public static World? b2world;
    static float timeStep = 1f / 60f;
    static int velocityIterations = 7;
    static int positionIterations = 4;

    static bool enableWorld = false;

    public static void InitializePhysicsWorld()
    {
        b2world = new World(gravity);
        
    }

    public static Body AddBodyToWorld(BodyDef body)
    {
        Body returnBody = b2world.CreateBody(body);
        return returnBody;
    }

    public static void ToggleWorldSimulation()
    {
        if(enableWorld)
        {
            enableWorld = false;
            DonutEngine.Backbone.Systems.DonutSystems.Update -= UpdateStep;
        }
        else
        {
            enableWorld = true;
            DonutEngine.Backbone.Systems.DonutSystems.Update += UpdateStep;
        }
    }

    public static void UpdateStep()
    {
        b2world.Step(timeStep, velocityIterations, positionIterations);
    }

}
