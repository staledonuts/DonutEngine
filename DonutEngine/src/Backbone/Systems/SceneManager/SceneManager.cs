namespace DonutEngine.Backbone.Systems;

public static class SceneManager
{
    public static Scene currentScene = new LogoSplashScene();
    
    public static void InitScene()
    {
        currentScene.InitializeScene();
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
