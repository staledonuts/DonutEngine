namespace DonutEngine.Backbone.Systems.SceneManager;
sealed class Empty : Scene
{
    public static readonly Empty Scene = new();
    
    Empty() { }
    
 
    void Scene.Init(SceneManager fsm) { }
    
 
    void Scene.Enter() { }
    
 
    void Scene.Update(float deltaTime) { }
    
 
    void Scene.Exit() { }

    void Scene.DrawUpdate() { }
}