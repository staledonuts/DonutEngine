namespace DonutEngine.Backbone.Systems;

public static class DonutSystems
{
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
        DonutSystems.SubscribeSystem(Sys.uISystem);
    }

    public static void KillSystems()
    {
        DonutSystems.UnsubscribeSystem(Sys.windowSystem);
        DonutSystems.UnsubscribeSystem(Sys.uISystem);
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
