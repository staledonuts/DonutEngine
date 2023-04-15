using DonutEngine.Backbone.Systems.Input;
using Raylib_cs;
using Box2DX.Common;
using ImGuiNET;
using System.Numerics;
using DonutEngine.Backbone.Systems.UI.Creator;
namespace DonutEngine.Backbone.Systems.UI;

public class UISystem : SystemsClass
{
    static bool Open = true;
    static bool DebugOpen = false;
    static bool ImGuiDemoOpen = false;
    static GameStats gameStats = new();
    static SoundPlayer soundPlayer = new();
    static LoadingScreen loadingScreen = new();
    static EntityCreator entityCreator = new();
    static EntitySpawnList entitySpawnList = new();
    static RenderTexture2D UIRenderTexture;
    static Rectangle rect;
    

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
                UIRenderTexture = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
            }
            if(DebugOpen)
            {
                soundPlayer.DrawUpdate();
                gameStats.DrawUpdate();
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
            Raylib.DrawText(Raylib.GetFPS().ToString(), 12, (int)Sys.cameraHandler.donutcam.offset.Y + 24, 20, Color.WHITE);
            rlImGui.Begin();
            if(DebugOpen)
            {
                DebugMenu();
            }
            loadingScreen.Show();
            rlImGui.End();
            Raylib.EndTextureMode();
            Raylib.DrawTextureRec(UIRenderTexture.texture, rect, Vector2.Zero, Color.WHITE);
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
                Sys.entityManager.ReloadEntities();
            }        
        }
    }

    public override void Shutdown()
    {
        Raylib.UnloadRenderTexture(UIRenderTexture);
        rlImGui.Shutdown();
    }

    public override void Start()
    {
        rlImGui.Setup(true);
        entitySpawnList.Setup();
        rect = new(0,0,Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        UIRenderTexture = Raylib.LoadRenderTexture((int)rect.width, (int)rect.height);
        InputEventSystem.ToggleUI += ToggleDebugUI;
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ Started UI System ]------");
    }

    public void ToggleDebugUI(CBool toggle)
    {
        if(DebugOpen && toggle)
        {
            DebugOpen = false;
        }
        else if(!DebugOpen && toggle)
        {
            DebugOpen = true;
        }
    }

    public void ShowLoadingUI(bool setBool)
    {
        loadingScreen.Open = setBool;
    }

    public void SetLoadingItem(string item)
    {
        loadingScreen.SetLoadingItem(item);
    }

    private void DebugMenu()
    {
        
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
        if(entityCreator.Open)
        {
            entityCreator.Show();
        }
        if(entitySpawnList.Open)
        {
            entitySpawnList.Show();
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

            if (ImGui.BeginMenu("Assets"))
            {
                ImGui.MenuItem("Entity Creator", string.Empty, ref entityCreator.Open);

                ImGui.MenuItem("Entity Spawner", string.Empty, ref entitySpawnList.Open);

                ImGui.MenuItem("Sound Tester", string.Empty, ref soundPlayer.Open);

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

