using DonutEngine.Backbone.Systems.UI;
using Raylib_cs;
using Box2DX.Common;
using ImGuiNET;
using System.Numerics;
namespace DonutEngine.Backbone.Systems;

public class UISystem : SystemsClass
{
    static bool Open = false;
    static bool Quit = false;
    static RenderTexture2D UIRenderTexture;
    static Rectangle rect = new(0,0,Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
    static Texture2D viewPort;
    

    public override void Update()
    {
        if (!Open)
                return;
        if (Raylib.IsWindowResized())
            {
                Raylib.UnloadRenderTexture(UIRenderTexture);
                UIRenderTexture = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
            }
        UpdateUIPosition();
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
            rlImGui.Begin();
            DoMainMenu();
            ImGui.ShowDemoWindow();
            rlImGui.End();
            Raylib.EndTextureMode();
            
            //Raylib.DrawTexture(UIRenderTexture.texture, 0, 0, Color.WHITE);
            Raylib.DrawTextureQuad(UIRenderTexture.texture, new(1,-1), Vector2.Zero, rect, Color.WHITE);
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
        UIRenderTexture = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        rlImGui.Setup(true);
        InputEventSystem.ToggleUI += ToggleUI;
        System.Console.WriteLine("Started UI sys");
    }

    private void UpdateUIPosition()
    {

    }

    public static void ToggleUI(CBool toggle)
    {
        if(Open && toggle)
        {
            Open = false;
        }
        else if(!Open && toggle)
        {
            Open = true;
        }
    }

    private static void DoMainMenu()
    {
        if (ImGui.BeginMainMenuBar())
        {
            if (ImGui.BeginMenu("File"))
            {
                if (ImGui.MenuItem("Exit"))
                    Quit = true;

                ImGui.EndMenu();
                Raylib.CloseWindow();
            }

            if (ImGui.BeginMenu("Window"))
            {
                //ImGui.MenuItem("ImGui Demo", string.Empty, ref ImGuiDemoOpen);

                ImGui.EndMenu();
            }
            ImGui.EndMainMenuBar();
        }
    }
}

