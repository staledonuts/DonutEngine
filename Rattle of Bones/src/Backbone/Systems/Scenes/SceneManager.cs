namespace DonutEngine.Backbone.Systems;

public static class SceneManager
{
    public static Scene currentScene = new LogoSplashScene();
    
    public static void InitScene()
    {
        currentScene.InitializeScene();
    }

    public static PhysicsWorld GetRef()
    {
        if(currentScene is GameplayScene g)
        {
            return g.physicsWorld;
        }
        else return null;
    }

    public static void LoadScene(Scene scene)
    {
        currentScene = scene;
        InitScene();
    }

    public static void UnloadScene()
    {
        currentScene = currentScene.UnloadScene();
        InitScene();
    }
    
}
