namespace DonutEngine;
using DonutEngine.Backbone.Systems.SceneManager;
using DonutEngine.Backbone.Systems.UI;
using ImGuiNET;
using Raylib_cs;

public class MainMenuSceneScene : Scene
{
    
    SceneManager? sM = null;
    Rectangle rect;

    
    public void Enter()
    {

    }

    public void Exit()
    {

    }

    public void Init(SceneManager SM)
    {   
        sM = SM;
    }

    public void Update(float deltaTime)
    {
        if (Raylib.IsWindowResized())
        {
            rect = new(0,0,Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        }
    }
    public void DrawUpdate()
    {
        Raylib.ClearBackground(Color.BLACK);
        rlImGui.Begin();
        
        rlImGui.End();
    }   
}