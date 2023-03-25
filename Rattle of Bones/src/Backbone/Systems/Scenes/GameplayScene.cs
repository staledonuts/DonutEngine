namespace DonutEngine;
using DonutEngine.Backbone.Systems;
using DonutEngine.Backbone;
using static DonutEngine.Backbone.Systems.DonutSystems;
using Raylib_cs;

public class GameplayScene : Scene
{
    public Entity? Player;

    public override void InitializeScene()
    {
        DonutSystems.Update += this.Update;
        DonutSystems.DrawUpdate += this.DrawUpdate;
        DonutSystems.LateUpdate += this.LateUpdate;
        Player = entityManager.CreateEntity(DonutFilePaths.entities+"Player.ini");
        entityManager.CreateEntity(DonutFilePaths.entities+"ParticleTest.ini");
        //entityManager.CreateEntity(DonutFilePaths.entities+"Test.ini");
        
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
        Raylib.ClearBackground(Color.MAROON);
        Raylib.DrawText(Raylib.GetFPS().ToString(), 12, 24, 20, Color.BLACK);
        Raylib.DrawRectangleV(new(0.0f, 100.0f),new(500,30), Raylib_cs.Color.BLACK);
    }
    public override void LateUpdate()
    {
        cameraHandler.UpdateCameraPlayerBoundsPush(ref cameraHandler.donutcam, Player.entityPhysics.GetVector2Pos(), 1f, settingsVars.screenWidth, settingsVars.screenHeight);
    }

    public override void Update()
    {
        
    }  
}