namespace DonutEngine;
using DonutEngine.Backbone.Systems;
public abstract partial class Scene
{

    public abstract void InitializeScene();

    public abstract Scene UnloadScene();

    public abstract void Update();

    public abstract void DrawUpdate();

    public abstract void LateUpdate();


}
