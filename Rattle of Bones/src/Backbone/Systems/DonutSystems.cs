namespace DonutEngine.Backbone.Systems;

public static class DonutSystems
{
    public static readonly SettingsVars settingsVars = new(DonutFilePaths.settings);
    public static readonly AudioControl audioControl = new();
    public static readonly WindowSystem windowSystem = new();
    public static readonly UISystem uISystem = new();
    public static readonly EntityManager entityManager = new();
    public static readonly LevelDataSystem levelDataSystem = new();
    public static readonly PhysicsSystem physicsSystem = new();
    public static CameraHandler cameraHandler = new();


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
        DonutSystems.SubscribeSystem(windowSystem);
        cameraHandler.InitializeCamera(new(0,0));
        DonutSystems.SubscribeSystem(audioControl);
        DonutSystems.SubscribeSystem(physicsSystem);
        DonutSystems.SubscribeSystem(uISystem);
        SceneManager.InitScene();
    }

    public static void KillSystems()
    {
        DonutSystems.UnsubscribeSystem(windowSystem);
        DonutSystems.UnsubscribeSystem(audioControl);
        DonutSystems.UnsubscribeSystem(physicsSystem);
        DonutSystems.UnsubscribeSystem(uISystem);
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
