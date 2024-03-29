using Engine.Systems.SceneSystem;
using Engine.FlatPhysics;
using Engine.EntityManager;
using Raylib_cs;
using System.Numerics;

namespace Template.Scenes;

public class Gameplay : Scene
{
    const int iterations = 6;
    public Gameplay()
    {
        entitiesData = new();
    }
    EntitiesData entitiesData;
    public override void DrawScene()
    {
        entitiesData.DrawUpdate();
    }

    public override void InitScene()
    {
        entitiesData.CreateEntity(new Player(new FlatBody(Vector2.Zero, 1, 2, 0.3f, 1,false, 2, 2, 2, ShapeType.Circle)));
    }

    public override void LateUpdateScene()
    {
        entitiesData.world.Step(Raylib.GetFrameTime(), iterations);
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