namespace DonutEngine;
using DonutEngine.Backbone.Systems;
using Raylib_cs;

public class GameplayScene : Scene
{

    public override void InitializeScene()
    {
        DonutSystems.SubscribeSystem(DonutSystems.levelDataSystem);
        DonutSystems.Update += this.Update;
        DonutSystems.DrawUpdate += this.DrawUpdate;
        DonutSystems.LateUpdate += this.LateUpdate;
        //entityManager.CreateEntity(DonutFilePaths.entities+"Player.json");
        DonutSystems.entityManager.CreateDirectory();
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
        
    }

    public override void Update()
    {
        
    }  
}