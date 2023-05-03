namespace DonutEngine;
using DonutEngine.Backbone.Systems;
using DonutEngine.Backbone.Systems.SceneManager;
using Raylib_cs;
using Box2DX.Dynamics;
public class GameplayScene : Scene
{
    Texture2D tex;

    public void DrawUpdate()
    {
        Raylib.DrawTextureRec(tex, new(0,0,Raylib.GetScreenWidth(), Raylib.GetScreenWidth()), System.Numerics.Vector2.Zero, Raylib_cs.Color.WHITE );
    }

    public void Init(SceneManager SM)
    {
        
    }

    public void Enter()
    {
        tex = Sys.textureContainer.GetTexture("background");
        Sys.entityManager.CreateDirectory();
    }

    public void Update(float deltaTime)
    {
        
    }

    public void Exit()
    {
        
    }
}