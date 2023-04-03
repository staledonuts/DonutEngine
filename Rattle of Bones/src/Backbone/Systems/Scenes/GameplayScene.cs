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
        
    }
    public override void LateUpdate()
    {
        cameraHandler.UpdateCameraPlayerBoundsPush(ref cameraHandler.donutcam, Player.currentBody.GetPosition(), 1f, settingsVars.screenWidth, settingsVars.screenHeight);
    }

    public override void Update()
    {
        
    }  
}