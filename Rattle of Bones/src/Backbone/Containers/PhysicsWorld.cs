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
    static World? b2world;

    static bool enableWorld = false;

    public static void InitializePhysicsWorld()
    {
        b2world = new World(gravity);
        
    }

    public static BodyDef AddBodyToWorld(BodyDef body)
    {
        Body returnBody = b2world.CreateBody(body);
        return body;
    }

    public static void ToggleWorldSimulation()
    {
        if(enableWorld)
        {
            enableWorld = false;
            DonutEngine.Backbone.Systems.DonutSystems.Update -= b2world.Step()
        }
        else
        {
            enableWorld = true;
            DonutEngine.Backbone.Systems.DonutSystems.Update += b2world.Step()
        }
    }

}
