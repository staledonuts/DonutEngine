namespace DonutEngine.Backbone.Systems;

public static class DonutSystems
{

    public delegate void Init();
    public delegate void Shutdown();
    public delegate void SystemsUpdater();
    public delegate void SystemsDrawUpdater();
    public delegate void SystemsLateUpdater();
    public static event Init? InitEvent;
    public static event Shutdown? ShutdownEvent;
    public static event SystemsUpdater? Update;
    public static event SystemsDrawUpdater? DrawUpdate;
    public static event SystemsLateUpdater? LateUpdate;

    public static void InitSystems()
    {
        InitEvent?.Invoke();
    }

    public static void ShutdownSystems()
    {
        ShutdownEvent?.Invoke();
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