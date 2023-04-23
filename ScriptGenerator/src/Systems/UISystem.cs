using Raylib_cs;
using ImGuiNET;
using System.Numerics;
using DonutEngine.Backbone.Systems.UI.Creator;
using DonutEngine.Backbone.Systems.UI.Audio;
namespace DonutEngine.Backbone.Systems.UI;

public class UISystem : SystemsClass
{
    RenderTexture2D UIRenderTexture;
    Rectangle rect = new(0,0,Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

    EntityCreator entityCreatorWindow = new();
    SoundDefiner soundDefinerWindow = new();

    

    public override void Update()
    {
        if (Raylib.IsWindowResized())
        {
            Raylib.UnloadRenderTexture(UIRenderTexture);
            rect = new(0,0,Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
            UIRenderTexture = Raylib.LoadRenderTexture((int)rect.width, (int)rect.height * -1);
        }
    }
    public override void DrawUpdate()
    {
        Raylib.BeginTextureMode(UIRenderTexture);
        Raylib.ClearBackground(Color.BLANK);
        rlImGui.Begin();
        MainMenu();
        rlImGui.End();
        Raylib.EndTextureMode();
        Raylib.DrawTextureRec(UIRenderTexture.texture, rect, Vector2.Zero, Color.WHITE);

    }

    public override void LateUpdate()
    {
        
    }

    public override void Shutdown()
    {
        rlImGui.Shutdown();
        Raylib.UnloadRenderTexture(UIRenderTexture);
    }

    public override void Start()
    {
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ Setting up UI System ]------");
        rlImGui.Setup(true);
        rect = new(0,0,Raylib.GetScreenWidth(), -Raylib.GetScreenHeight());
        UIRenderTexture = Raylib.LoadRenderTexture((int)rect.width, (int)rect.height * -1);

        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ UI System Initialized ]------");
    }

    private void MainMenu()
    {
        
        DoMainMenu();
        if (entityCreatorWindow.Open)
        {
            entityCreatorWindow.DrawUpdate();
        }
        if (soundDefinerWindow.Open)
        {
            soundDefinerWindow.DrawUpdate();
        }
    }

    private void DoMainMenu()
    {
        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("File"))
            {
                if (ImGui.MenuItem("Exit"))
                {
                    Raylib.CloseWindow();
                }
                ImGui.EndMenu();
            }
            if (ImGui.BeginMenu("Entities"))
            {
                if (ImGui.MenuItem("Entity Creator", string.Empty, ref entityCreatorWindow.Open))
                {
                    
                }
                ImGui.EndMenu();
            }
            if (ImGui.BeginMenu("Audio"))
            {
                if (ImGui.MenuItem("SFX Definer", string.Empty, ref soundDefinerWindow.Open))
                {
                    
                }
                ImGui.EndMenu();
            }
            ImGui.EndMainMenuBar();
        }
    }
}

