namespace Engine.Systems.SceneSystem;

using Engine.EntityManager;
using Engine.Exceptions;
using Engine.Systems;
using Raylib_cs;


public class SceneManager : SystemClass, IUpdateSys, IDrawUpdateSys, ILateUpdateSys
{
    public void DrawUpdate()
    {
        if(_current is not null)
        {
            _current.DrawScene();
        }
    }

    public void LateUpdate()
    {
        if(_current is not null)
        {
            _current.LateUpdateScene();
        }
    }

    public override void Shutdown()
    {
        if(_current is not null)
        {
            _current.UnloadScene();
        }
    }

    public void Update()
    {
        if(_current is not null)
        {
            _current.UpdateScene();
        }
    }

    Dictionary<Int64, Scene> _Scenes = new();
    Scene _current = Empty.Scene;

    public EntitiesData GetEntitiesData()
    {
        return _current.entitiesData;
    }

    Scene this[Int64 index]
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



    public void SwitchTo(Int64 key) => SwitchTo(this[key]);

    public void SwitchTo(Scene Scene)
    {
        Raylib.TraceLog(TraceLogLevel.Info, "Loading new level");
        try
        {
            _current.UnloadScene();
            _current = Scene;
            _current.InitScene();
            Raylib.TraceLog(TraceLogLevel.Info, "Level Loaded");
        }
        catch(Exception e)
        {
            _current = Empty.Scene;
            Raylib.TraceLog(TraceLogLevel.Error, "Level Loading Failed: "+e);
        }
    }

    public override void Initialize()
    {
        AddScene(1, new Logo());
        SwitchTo(1);
    }
}
