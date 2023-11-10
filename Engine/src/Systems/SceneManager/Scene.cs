namespace Engine.Systems.SceneSystem;


public abstract class Scene
{

    public abstract void InitScene();
    public abstract void UpdateScene();
    public abstract void DrawScene();
    public abstract void LateUpdateScene();
    public abstract void UnloadScene();
}
