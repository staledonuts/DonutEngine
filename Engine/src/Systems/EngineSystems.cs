#nullable disable
using Engine.Assets;
using Engine.Logging;
using Engine.Utils;
using Raylib_CSharp.Logging;
namespace Engine.Systems;
public static class EngineSystems
{
    
    static EngineSystems()
    {
        systems = new Dictionary<Type, SystemClass>();
    }
    
    static TimeSpan time = new();
    private readonly static Dictionary<Type, SystemClass> systems;
    delegate void delegateUpdate();
    static delegateUpdate dUpdate;
    delegate void delegateDrawUpdate();
    static delegateDrawUpdate dDrawUpdate;
    delegate void delegateLateUpdate();
    static delegateLateUpdate dLateUpdate;
    public delegate void delegateMiscUpdate();
    public static delegateMiscUpdate dMiscUpdate;


    public static void AddSystem(SystemClass systemClass)
    {
        if (systemClass != null)
        {
            if(systemClass is IUpdateSys upd)
            {
                dUpdate += upd.Update;
            }
            if(systemClass is IDrawUpdateSys drw)
            {
                dDrawUpdate += drw.DrawUpdate;
            }
            if(systemClass is ILateUpdateSys lte)
            {
                dLateUpdate += lte.LateUpdate;
            }
            // Add the system to the dictionary with its type as the key
            systems[systemClass.GetType()] = systemClass;
            Logger.TraceLog(TraceLogLevel.Info, systemClass.GetType().Name+" was added as a System");
            systemClass.Initialize();
        }
    }

    public static void RemoveSystem<T>() where T : SystemClass
    {
        
        if(systems.ContainsKey(typeof(T)))
        {
            SystemClass system = systems.GetValueOrDefault(typeof(T));            
            if(system is IUpdateSys upd)
            {
                dUpdate -= upd.Update;
            }
            if(system is IDrawUpdateSys drw)
            {
                dDrawUpdate -= drw.DrawUpdate;
            }
            if(system is ILateUpdateSys lte)
            {
                dLateUpdate -= lte.LateUpdate;
            }
            GetSystem<T>().Shutdown();
            systems.Remove(typeof(T));
        }
    }

    public static T GetSystem<T>() where T : SystemClass
    {
        
        if(systems.TryGetValue(typeof(T), out SystemClass systemClass))
        {
            return (T)systemClass;
        }
        return null;
    }

    public static void Update()
    {
        EngineTime.Advance(time);
        if(dUpdate != null)
        {
            dUpdate();
        }
    }

    public static void DrawUpdate()
    {
        if(dDrawUpdate != null)
        {
            dDrawUpdate();
        }
    }

    public static void LateUpdate()
    {
        if(dLateUpdate != null)
        {
            dLateUpdate();
        }
    }

    public static void MiscUpdate()
    {
        if(dMiscUpdate != null)
        {
            dMiscUpdate();
        }
    }

    public static void ShutdownSystems()
    {
        foreach(KeyValuePair<Type, SystemClass> key in systems)
        {
            key.Value.Shutdown();
        }
        systems.Clear();
        Textures.FlushTextureLibrary();
        Fonts.FlushFontLibrary();
    }

}
public abstract class SystemClass
{
    public abstract void Initialize();
    public abstract void Shutdown();
}

public interface IUpdateSys
{
    public void Update();
}

public interface IDrawUpdateSys
{
    public void DrawUpdate();
}

public interface ILateUpdateSys
{
    public void LateUpdate();
}


