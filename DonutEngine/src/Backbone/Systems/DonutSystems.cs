namespace DonutEngine.Backbone.Systems;
/// <summary>
/// The Systems Updater: - 
/// <example>
/// a simple event system you subscribe all SystemsClasses to for the main update loop.
/// Currently DonutEngine.Sys Contains the Different system classes for convenient access.
/// The Three Booleans below is for Subscribing to the different Update Loops;
/// Dont forget to Unsubscribe too!
/// <code>
/// DonutSystems.SubscribeSystem("Sys.systemsClass", true, true, false);
/// </code>
/// </example>
/// </summary>

public static class DonutSystems
{
    /////////////////////////////////////////////////
    //
    // 
    /////////////////////////////////////////////////
    
    public delegate void SystemsUpdater();
    public delegate void SystemsDrawUpdater();
    public delegate void SystemsLateUpdater();
    public static event SystemsUpdater? Update;
    public static event SystemsDrawUpdater? DrawUpdate;
    public static event SystemsLateUpdater? LateUpdate;

    public static void SubscribeSystem(SystemsClass systemsClass, bool SubUpdate, bool SubDraw, bool SubLate)
    {
        systemsClass.Start();
        if(SubUpdate)
        {
            DonutSystems.Update += systemsClass.Update;
        }
        if(SubDraw)
        {
            DonutSystems.DrawUpdate += systemsClass.DrawUpdate;
        }
        if(SubLate)
        {
            DonutSystems.LateUpdate += systemsClass.LateUpdate;
        }
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
        DonutSystems.SubscribeSystem(Sys.windowSystem, true, true, false);
        Sys.textureContainer.InitTextureContainer();
        Sys.cameraHandler.InitializeCamera(new(0,0));
        DonutSystems.SubscribeSystem(Sys.audioControl, false, false, true);
        DonutSystems.SubscribeSystem(Sys.physicsSystem, true, false, false);
        DonutSystems.SubscribeSystem(Sys.uISystem, true, true, true);
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
