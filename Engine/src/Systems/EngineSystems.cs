#nullable disable
using Engine.Assets;
using Engine.Logging;
using Raylib_cs;
namespace Engine.Systems;
public static class EngineSystems
{
    
    static EngineSystems()
    {
        systems = new Dictionary<Type, SystemClass>();
    }
    

    private readonly static Dictionary<Type, SystemClass> systems;
    public delegate void delegateUpdate();
    public static delegateUpdate dUpdate;
    public delegate void delegateDrawUpdate();
    public static delegateDrawUpdate dDrawUpdate;
    public delegate void delegateLateUpdate();
    public static delegateLateUpdate dLateUpdate;

    public static void AddSystem(SystemClass systemClass)
    {
        if (systemClass != null)
        {
            // Add the system to the dictionary with its type as the key
            systems[systemClass.GetType()] = systemClass;
            Raylib.TraceLog(Raylib_cs.TraceLogLevel.LOG_INFO, systemClass.GetType().Name+" was added as a System");
            systemClass.Initialize();
        }
    }

    public static void RemoveSystem<T>() where T : SystemClass
    {
        if(systems.ContainsKey(typeof(T)))
        {
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
        if(dUpdate is not null)
        {
            dUpdate();
        }
    }

    public static void DrawUpdate()
    {
        if(dDrawUpdate is not null)
        {
            dDrawUpdate();
        }
    }

    public static void LateUpdate()
    {
        if(dLateUpdate is not null)
        {
            dLateUpdate();
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
    public abstract void Update();
    public abstract void DrawUpdate();
    public abstract void LateUpdate();
    public abstract void Shutdown();
}


