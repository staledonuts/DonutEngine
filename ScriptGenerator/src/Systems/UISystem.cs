using Raylib_cs;
using ImGuiNET;
using System.Numerics;
using DonutEngine.Backbone.Systems.UI.Creator;
namespace DonutEngine.Backbone.Systems.UI;

public class UISystem : SystemsClass
{
    static bool Open = true;
    static bool ImGuiDemoOpen = false;
    static RenderTexture2D UIRenderTexture;
    static Rectangle rect = new(0,0,Raylib.GetScreenWidth(), Raylib.GetScreenHeight());

    static EntityCreator sceneViewWindow = new();
    

    public override void Update()
    {
        if (!Open)
        {
            return;
        }
        else if(Open)
        {
            if (Raylib.IsWindowResized())
            {
                Raylib.UnloadRenderTexture(UIRenderTexture);
                rect = new(0,0,Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
                UIRenderTexture = Raylib.LoadRenderTexture((int)rect.width, (int)rect.height * -1);
            }
        }
    }
    public override void DrawUpdate()
    {
        if (!Open)
        {
            return;
        }
        else
        {
            Raylib.BeginTextureMode(UIRenderTexture);
            Raylib.ClearBackground(Color.BLANK);
            //Raylib.DrawText(Time.deltaTime.ToString(), 12, (int)Sys.cameraHandler.donutcam.offset.Y + 24, 20, Color.WHITE);
            rlImGui.Begin();
            /*if(MenuOpen)
            {
                MainMenu();
            }*/
            //loadingScreen.Show();
            rlImGui.End();
            Raylib.EndTextureMode();
            Raylib.DrawTextureRec(UIRenderTexture.texture, rect, Vector2.Zero, Color.WHITE);
        }
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

    private static void DoMainMenu()
    {
        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("File"))
            {
                if (ImGui.MenuItem("Entity Factory"))
                {
                    
                }
                if (ImGui.MenuItem("Exit"))
                {
                    Raylib.CloseWindow();
                }

                ImGui.EndMenu();

            }

            if (ImGui.BeginMenu("Window"))
            {
                //ImGui.MenuItem("Reload Entities", string.Empty, )
                ImGui.MenuItem("ImGui Demo", string.Empty, ref ImGuiDemoOpen);

                ImGui.EndMenu();
            }
            ImGui.EndMainMenuBar();
        }
    }
}

