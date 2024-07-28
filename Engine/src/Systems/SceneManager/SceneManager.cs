namespace Engine.Systems.SceneSystem;

using Engine.Entities;
using Engine.EntityManager;
using Engine.Exceptions;
using Engine.Systems;
using Raylib_CSharp.Logging;

public class SceneManager : SystemClass, IUpdateSys, IDrawUpdateSys, ILateUpdateSys
{
    public void DrawUpdate()
    {
        if(_current != null)
        {
            _current.DrawScene();
        }
    }

    public void LateUpdate()
    {
        if(_current != null)
        {
            _current.LateUpdateScene();
        }
    }

    public override void Shutdown()
    {
        if(_current != null)
        {
            _current.UnloadScene();
        }
    }

    public void Update()
    {
        if(_current != null)
        {
            _current.UpdateScene();
        }
    }

    Dictionary<long, Scene> _Scenes = new();
    Scene _current = Empty.Scene;

    public IEntitiesData GetEntitiesData()
    {
        return _current.entitiesData;
    }

    Scene this[long index]
    {
        get
        {
            if (!_Scenes.ContainsKey(index))
                throw new MissingSceneException($"Scene of type {index} does not exist in Finite Scene Machine of type {this}!");
            return _Scenes[index];
        }
    }

    /*public SceneManager(params (Int64, Scene)[] Scenes)
    {
        foreach (var (key, value) in Scenes) 
            _Scenes.Add(key, value);
        foreach (var (_, value) in _Scenes) 
            value.Init(this);
    }*/

    public void AddScene(int sceneNumber, Scene sceneClass)
    {
        _Scenes.Add(sceneNumber, sceneClass);
    }



    public void SwitchTo(long key) => SwitchTo(this[key]);

    public void SwitchTo(Scene Scene)
    {
       Logger.TraceLog(TraceLogLevel.Info, "Loading new level");
        try
        {
            _current.UnloadScene();
            _current = Scene;
            _current.InitScene();
            Logger.TraceLog(TraceLogLevel.Info, "Level Loaded");
        }
        catch(Exception e)
        {
            _current = Empty.Scene;
            Logger.TraceLog(TraceLogLevel.Error, "Level Loading Failed: "+e);
        }
    }

    public override void Initialize()
    {
        AddScene(1, new Logo());
        SwitchTo(1);
    }
}
