namespace DonutEngine.Backbone.Systems;
using System.Numerics;
using Box2D.NetStandard.Common;
using Box2D.NetStandard.Dynamics.World;
using Box2D.NetStandard.Dynamics.Bodies;
using Box2D.NetStandard.Dynamics.Fixtures;
using Box2D.NetStandard.Collision.Shapes;
using Box2D.NetStandard.Collision;
using Raylib_cs;

public class PhysicsWorld
{
    Level level;


    public void InitializePhysicsWorld()
    {
        level = new();
    }

    public Body AddBodyToWorld(BodyDef body)
    {
        Body returnBody = level.b2world.CreateBody(body);
        if(returnBody is not null)
        { 
            returnBody.SetLinearVelocity(new(10,10));
            return returnBody;
        }
        return returnBody;
    }

    public void UpdateStep()
    {
        level.b2world.Step(level.timeStep, level.velocityIterations, level.positionIterations);

    }

    public void DrawUpdate()
    {
        Raylib.DrawRectangleV(level.groundBodyDef.position,new(50,10), Raylib_cs.Color.BLACK);
    }

    private struct Level
    {
        public Level()
        {
            setGravity = new(0, -10);
            timeStep = 1f / 60f;
            velocityIterations = 7;
            positionIterations = 4;
            b2world = new World(setGravity);
            groundBodyDef = new();
            groundBodyDef.position = new(0.0f, -10.0f);
            groundBody = b2world.CreateBody(groundBodyDef);
            groundBox = new();
            groundBox.SetAsBox(50f, 10f);
            groundBody.CreateFixture(groundBox, 0f);
        }
        public BodyDef groundBodyDef;
        public Body groundBody;
        PolygonShape groundBox;
        public Vector2 setGravity;
        public World? b2world;
        public readonly float timeStep;
        public readonly int velocityIterations;
        public readonly int positionIterations;
    }

}
