namespace DonutEngine;
using DonutEngine.Backbone.Systems;
using Raylib_cs;

public class GameplayScene : Scene
{
    public PhysicsWorld physicsWorld; 

    public override void InitializeScene()
    {
        physicsWorld = new();
        physicsWorld.InitializePhysicsWorld();
        DonutSystems.Update += this.Update;
        DonutSystems.DrawUpdate += this.DrawUpdate;
        DonutSystems.LateUpdate += this.LateUpdate;
    }

    public override Scene UnloadScene()
    {
        DonutSystems.Update -= this.Update;
        DonutSystems.DrawUpdate -= this.DrawUpdate;
        DonutSystems.LateUpdate -= this.LateUpdate;
        return null;
    }

    public override void DrawUpdate()
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText(Raylib.GetFPS().ToString(), 12, 24, 20, Color.BLACK);
    }
    public override void LateUpdate()
    {
        //throw new NotImplementedException();
    }

    public override void Update()
    {
        physicsWorld.UpdateStep();
    }

    
}