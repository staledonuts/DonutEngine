namespace Engine.Systems.SceneSystem;
sealed class Empty : Scene
{
    public static readonly Empty Scene = new();
    
    Empty() 
    {
        //entitiesData = new EntitiesData2D();
        //entitiesData = new EntitiesData3D();
    }

    public override void DrawScene()
    {
        
    }

    public override void InitScene()
    {
        
    }

    public override void LateUpdateScene()
    {
        
    }

    public override void UnloadScene()
    {
        
    }

    public override void UpdateScene()
    {
        
    }
}