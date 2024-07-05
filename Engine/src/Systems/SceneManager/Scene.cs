using Engine.Assets;
using Engine.EntityManager;

namespace Engine.Systems.SceneSystem;


public abstract class Scene : IDisposable
{
    public EntitiesData entitiesData = new();

    public abstract void InitScene();
    public abstract void UpdateScene();
    public abstract void DrawScene();
    public abstract void LateUpdateScene();
    public abstract void UnloadScene();

    public void Dispose()
    {
        entitiesData.WipeEntities();
        Textures.UnloadTextureLibrary();
        GC.SuppressFinalize(this);
    }
}
