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
    static bool MenuOpen = false;
    static bool ImGuiDemoOpen = false;
    #if DEBUG
    static GameStats gameStats = new();
    #endif
    static SoundPlayer soundPlayer = new();
    static LoadingScreen loadingScreen = new();
    static EntityCreator entityCreator = new();
    static EntitySpawnList entitySpawnList = new();
    static RenderTexture2D UIRenderTexture;
    static Rectangle rect;
    
    public override void Start()
    {
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ Setting up UI System ]------");
        rlImGui.Setup(true);
        entitySpawnList.Setup();
        rect = new(0,0,Raylib.GetScreenWidth(), -Raylib.GetScreenHeight());
        UIRenderTexture = Raylib.LoadRenderTexture((int)rect.width, (int)rect.height * -1);
        InputEventSystem.ToggleUI += ToggleDebugUI;
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ UI System Initialized ]------");
    }

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
            if(MenuOpen)
            {
                soundPlayer.DrawUpdate();
                #if DEBUG
                gameStats.DrawUpdate();
                #endif
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
            Raylib.DrawText(Time.deltaTime.ToString(), 12, (int)Sys.cameraHandler.donutcam.offset.Y + 24, 20, Color.WHITE);
            rlImGui.Begin();
            if(MenuOpen)
            {
                MainMenu();
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

    public void ToggleDebugUI(CBool toggle)
    {
        if(MenuOpen && toggle)
        {
            MenuOpen = false;
        }
        else if(!MenuOpen && toggle)
        {
            MenuOpen = true;
        }
    }
    private void MainMenu()
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
            #if DEBUG
            if (ImGui.BeginMenu("Debug"))
            {
                ImGui.MenuItem("General", "F3", ref gameStats.Open);
                ImGui.MenuItem("FPS:"+Raylib.GetFPS().ToString());
                ImGui.EndMenu();
            }
            #endif
            ImGui.EndMainMenuBar();
        }
    }
}

