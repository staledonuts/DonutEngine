#nullable disable

namespace DonutEngine;
public static class Systems
{
    private readonly static Dictionary<Type, SystemClass> _systems = new Dictionary<Type, SystemClass>();
    private delegate void delegateUpdate();
    private static delegateUpdate dUpdate;
    private delegate void delegateDrawUpdate();
    private static delegateDrawUpdate dDrawUpdate;
    private delegate void delegateLateUpdate();
    private static delegateLateUpdate dLateUpdate;

    private static bool _initialized;

    public static void InitializeCoreSystems()
    {
        if(!_initialized)
        {
            _initialized = true;
        }
        Add(new WorldSystem());
        Add(new LdtkSystem(Systems.Get<WorldSystem>()));
    }

    public static void Add(SystemClass systemClass)
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
            _systems[systemClass.GetType()] = systemClass;
            systemClass.Initialize();
        }
    }

    public static void Remove<T>() where T : SystemClass
    {
        
        if(_systems.ContainsKey(typeof(T)))
        {
            SystemClass system = _systems.GetValueOrDefault(typeof(T));            
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
            Get<T>().Shutdown();
            _systems.Remove(typeof(T));
        }
    }

    public static T Get<T>() where T : SystemClass
    {
        
        if(_systems.TryGetValue(typeof(T), out SystemClass systemClass))
        {
            return (T)systemClass;
        }
        return null;
    }

    public static void Update()
    {
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

    public static void ShutdownSystems()
    {
        foreach(KeyValuePair<Type, SystemClass> key in _systems)
        {
            key.Value.Shutdown();
        }
        _systems.Clear();
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



