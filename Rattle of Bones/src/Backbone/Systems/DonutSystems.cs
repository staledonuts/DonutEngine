namespace DonutEngine.Backbone.Systems;

public static class DonutSystems
{

    public delegate void Init();
    public delegate void Shutdown();
    public delegate void SystemsUpdater();
    public static event Init? InitEvent;
    public static event Shutdown? ShutdownEvent;
    public static event SystemsUpdater? Update;

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
}
