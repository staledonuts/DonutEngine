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
        DonutSystems.SubscribeSystem(DonutSystems.levelDataSystem);
        DonutSystems.Update += this.Update;
        DonutSystems.DrawUpdate += this.DrawUpdate;
        DonutSystems.LateUpdate += this.LateUpdate;
        Player = entityManager.CreateEntity(DonutFilePaths.entities+"Player.json");
        //entityManager.CreateEntity(DonutFilePaths.entities+"ParticleTest.ini");
    }

    public override Scene UnloadScene()
    {
        DonutSystems.UnsubscribeSystem(DonutSystems.levelDataSystem);
        DonutSystems.Update -= this.Update;
        DonutSystems.DrawUpdate -= this.DrawUpdate;
        DonutSystems.LateUpdate -= this.LateUpdate;
        return this;
    }

    public override void DrawUpdate()
    {
        
        Raylib.DrawText(Raylib.GetFPS().ToString(), 12, 24, 20, Color.BLACK);
        Raylib.DrawRectangleV(new(0.0f, 100.0f),new(500,30), Raylib_cs.Color.BLACK);
    }
    public override void LateUpdate()
    {
        cameraHandler.UpdateCameraPlayerBoundsPush(ref cameraHandler.donutcam, Player.currentBody.GetPosition(), 1f, settingsVars.screenWidth, settingsVars.screenHeight);
    }

    public override void Update()
    {
        
    }  
}