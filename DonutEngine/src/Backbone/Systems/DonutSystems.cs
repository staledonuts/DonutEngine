namespace DonutEngine.Backbone.Systems;


public static class DonutSystems
{
    /////////////////////////////////////////////////
    //This is my Systems Updater
    // a simple event system that i subscribe all the systems to for the update loop.
    /////////////////////////////////////////////////
    


    public delegate void SystemsUpdater();
    public delegate void SystemsDrawUpdater();
    public delegate void SystemsLateUpdater();
    public static event SystemsUpdater? Update;
    public static event SystemsDrawUpdater? DrawUpdate;
    public static event SystemsLateUpdater? LateUpdate;

    public static void SubscribeSystem(SystemsClass systemsClass)
    {
        systemsClass.Start();
        DonutSystems.Update += systemsClass.Update;
        DonutSystems.DrawUpdate += systemsClass.DrawUpdate;
        DonutSystems.LateUpdate += systemsClass.LateUpdate;
    }

    public static void UnsubscribeSystem(SystemsClass systemsClass)
    {
        DonutSystems.Update -= systemsClass.Update;
        DonutSystems.DrawUpdate -= systemsClass.DrawUpdate;
        DonutSystems.LateUpdate -= systemsClass.LateUpdate;
        systemsClass.Shutdown();
    }

    public static void InitSystems()
    {
        DonutSystems.SubscribeSystem(Sys.windowSystem);
        Sys.textureContainer.InitTextureContainer();
        Sys.shaderSystem.InitShaders();
        Sys.cameraHandler.InitializeCamera(new(0,0));
        DonutSystems.SubscribeSystem(Sys.audioControl);
        DonutSystems.SubscribeSystem(Sys.physicsSystem);
        DonutSystems.SubscribeSystem(Sys.uISystem);
        SceneManager.InitScene();
    }

    public static void KillSystems()
    {
        DonutSystems.UnsubscribeSystem(Sys.uISystem);
        DonutSystems.UnsubscribeSystem(Sys.physicsSystem);
        DonutSystems.UnsubscribeSystem(Sys.audioControl);
        Sys.textureContainer.EmptyTextureLibrary();
        DonutSystems.UnsubscribeSystem(Sys.windowSystem);
    }

    public static void UpdateSystems()
    {
        Update?.Invoke();
    }

    public static void UpdateDraw()
    {
        DrawUpdate?.Invoke();
    }

    public static void UpdateLate()
    {
        LateUpdate?.Invoke();
    }

    
}



public abstract class SystemsClass
{
    public abstract void Start();

    public abstract void Update();

    public abstract void DrawUpdate();

    public abstract void LateUpdate();

    public abstract void Shutdown();
}
