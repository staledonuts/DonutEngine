using Engine.Systems.SceneSystem;
using Engine.FlatPhysics;
using Engine.EntityManager;
using Raylib_cs;

namespace Template.Scenes;

public class Gameplay : Scene
{
    const int iterations = 6;
    public Gameplay()
    {
        world = new();
        entitiesData = new();
    }
    FlatWorld world;
    EntitiesData entitiesData;
    public override void DrawScene()
    {
        entitiesData.DrawUpdate();
    }

    public override void InitScene()
    {
        
    }

    public override void LateUpdateScene()
    {
        world.Step(Raylib.GetFrameTime(), iterations);
        entitiesData.LateUpdate();
    }

    public override void UnloadScene()
    {
        
    }

    public override void UpdateScene()
    {
        entitiesData.Update();
    }
}