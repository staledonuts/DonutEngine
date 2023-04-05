using DonutEngine.Backbone.Systems.UI;
using Raylib_cs;
using Box2DX.Common;
using ImGuiNET;
using System.Numerics;
namespace DonutEngine.Backbone.Systems;

public class UISystem : SystemsClass
{
    static bool Open = false;
    static bool ImGuiDemoOpen = false;
    static GameStats gameStats = new();
    static SoundPlayer soundPlayer = new();
    static RenderTexture2D UIRenderTexture;
    static Rectangle rect = new(0,0,Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
    

    public override void Update()
    {
        if (!Open)
        {
            return;
        }
        else
        {
            if (Raylib.IsWindowResized())
            {
                Raylib.UnloadRenderTexture(UIRenderTexture);
                UIRenderTexture = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
            }
            soundPlayer.Update();
            gameStats.Update();
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
            rlImGui.Begin();
            DoMainMenu();
            if (ImGuiDemoOpen)
            {
                ImGui.ShowDemoWindow(ref ImGuiDemoOpen);
            }
            if(soundPlayer.Open)
            {
                soundPlayer.Show();
            }
            if(gameStats.Open)
            {
                gameStats.Show();
            }
            rlImGui.End();
            Raylib.DrawText(Raylib.GetFPS().ToString(), (int) + 12, (int)DonutSystems.cameraHandler.donutcam.offset.Y + 24, 20, Color.BLACK);
            Raylib.EndTextureMode();
            Raylib.DrawTextureQuad(UIRenderTexture.texture, new(1,-1), Vector2.Zero, rect, Color.WHITE);
        }
    }

    public override void LateUpdate()
    {
        if (!Open)
        {
            return;
        }
        else
        {
            if(Raylib.IsKeyPressed(KeyboardKey.KEY_F5))
            {
                DonutSystems.entityManager.ReloadEntities();
            }        
        }
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
                if(ImGui.MenuItem("Reload Entities", "F5"))
                {
                    DonutSystems.entityManager.ReloadEntities();
                }
                if (ImGui.MenuItem("Exit"))
                {
                    Raylib.CloseWindow();
                }

                ImGui.EndMenu();
            }

            if (ImGui.BeginMenu("Assets"))
            {
                //ImGui.MenuItem("Reload Entities", string.Empty, )
                ImGui.MenuItem("Sound Player", string.Empty, ref soundPlayer.Open);

                ImGui.EndMenu();
            }

            if (ImGui.BeginMenu("Demo"))
            {
                ImGui.MenuItem("ImGui Demo", string.Empty, ref ImGuiDemoOpen);
                ImGui.EndMenu();
            }
            if (ImGui.BeginMenu("Game Stat"))
            {
                ImGui.MenuItem("General", "F3", ref gameStats.Open);
                ImGui.MenuItem("FPS:"+Raylib.GetFPS().ToString());
                ImGui.EndMenu();
            }
            ImGui.EndMainMenuBar();
        }
    }
}

