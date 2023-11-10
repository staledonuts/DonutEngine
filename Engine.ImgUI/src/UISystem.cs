namespace Engine.Systems.UI;
using Raylib_cs;
using ImGuiNET;

public class UISystem : SystemClass
{
    static bool ImGuiDemoOpen = false;
    static SettingsWindow settingsWindow = new();
    static Console console = new();
    static Rectangle rect;
    
    public override void Initialize()
    {
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ Setting up UI System ]------");
        rlImGui.Setup(true);
        ImGui.GetIO().ConfigWindowsMoveFromTitleBarOnly = true;
        settingsWindow.Setup();
        console.Setup();
        ImGui.DockSpace(1);
        rect = new(0,0,Raylib.GetScreenWidth(), -Raylib.GetScreenHeight());
        EngineSystems.dUpdate += Update;
        EngineSystems.dDrawUpdate += DrawUpdate;
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ UI System Initialized ]------");
    }

    public override void Update()
    {
        if (Raylib.IsWindowResized())
        {
            rect = new(0,0,Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        }
    }
    public override void DrawUpdate()
    {
        /*Raylib.ClearBackground(Color.BLANK);
        rlImGui.Begin();
        MainMenu();
        rlImGui.End();*/
    }

    public override void LateUpdate()
    {

    }

    public override void Shutdown()
    {
        EngineSystems.dUpdate -= Update;
        EngineSystems.dDrawUpdate -= DrawUpdate;
        settingsWindow.Shutdown();
        rlImGui.Shutdown();
    }

    private void MainMenu()
    {
        
        DoMainMenu();
        if (ImGuiDemoOpen)
        {
            ImGui.ShowDemoWindow(ref ImGuiDemoOpen);
        }
        if(settingsWindow.Open)
        {
            settingsWindow.Show();
        }
        if(console.Open)
        {
            console.Show();
        }
    }
    

    private void DoMainMenu()
    {
        if (ImGui.BeginMainMenuBar())
        {
            ImGui.MenuItem("Console", string.Empty, ref console.Open, true);
            ImGui.MenuItem("Settings", string.Empty, ref settingsWindow.Open);

            if (ImGui.BeginMenu("Demo"))
            {
                ImGui.MenuItem("ImGui Demo", string.Empty, ref ImGuiDemoOpen);
                ImGui.EndMenu();
            }
            ImGui.EndMainMenuBar();
        }
    }
}
