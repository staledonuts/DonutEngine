namespace DonutEngine;
using DonutEngine.Backbone.Systems;
using Raylib_cs;

public class GameplayScene : Scene
{
    public PhysicsWorld? physicsWorld; 
    public GameObjects? gameObjects;

    public override void InitializeScene()
    {
        physicsWorld = new();
        physicsWorld.InitializePhysicsWorld();
        gameObjects = new();
        DonutSystems.Update += this.Update;
        DonutSystems.DrawUpdate += this.DrawUpdate;
        DonutSystems.LateUpdate += this.LateUpdate;
        Program.cameraHandler.ChangeCameraTarget(gameObjects.player.physics2D.rigidbody2D.GetPosition());
    }

    public override Scene UnloadScene()
    {
        DonutSystems.Update -= this.Update;
        DonutSystems.DrawUpdate -= this.DrawUpdate;
        DonutSystems.LateUpdate -= this.LateUpdate;
        return this;
    }

    public override void DrawUpdate()
    {
        Raylib.ClearBackground(Color.WHITE);
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