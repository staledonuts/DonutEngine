namespace DonutEngine.Backbone.Systems.SceneManager;
using System.Collections.Generic;
using System.Collections;

public class SceneManager : SystemsClass
{
    Dictionary<Int64, Scene> _Scenes = new();
    Scene _current = Empty.Scene;

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

    void AddScene(int sceneNumber, Scene sceneClass)
    {
        _Scenes.Add(sceneNumber, sceneClass);
        sceneClass.Init(this);
    }



    public void SwitchTo(Int64 key) => SwitchTo(this[key]);

    public void SwitchTo(Scene Scene)
    {
        _current.Exit();
        _current = Scene ?? Empty.Scene;
        _current.Enter();
    }
    public override void Start()
    {
        AddScene(1, new DonutLogoSplashScene());
        AddScene(2, new GameplayScene());
        SwitchTo(1);
    }

    public override void Update()
    {
        _current.Update(Time.deltaTime);
    }

    public override void DrawUpdate()
    {
        _current.DrawUpdate();
    }

    public override void LateUpdate()
    {
        
    }

    public override void Shutdown()
    {
        
    }
}