namespace DonutEngine.Backbone.Systems.UI.Audio;
using Raylib_cs;
using ImGuiNET;
using System.Numerics;
public class SoundDefiner : DocumentWindow
{
    public override void DrawUpdate()
    {
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
        ImGui.SetNextWindowSizeConstraints(new Vector2(400, 400), new Vector2((float)Raylib.GetScreenWidth(), (float)Raylib.GetScreenHeight()));
        
        if (ImGui.Begin("Entity Creator", ref Open, ImGuiWindowFlags.MenuBar))
        {
            DoMenu();
            Focused = ImGui.IsWindowFocused(ImGuiFocusedFlags.RootAndChildWindows);
            Vector2 size = ImGui.GetContentRegionAvail();
            ImGui.End();
        }
        ImGui.PopStyleVar();
    }

    public override void Shutdown()
    {
        
    }

    public override void Start()
    {
        
    }

    public override void Update()
    {
        
    }

    private void DoMenu()
    {
        if(ImGui.BeginMenuBar())
        {
            if(ImGui.BeginMenu("File"))
            {
                if(ImGui.MenuItem(" [Reset]"))
                {
                    //ResetWindow();
                }
                if(ImGui.MenuItem(" [Save As]"))
                {
                    //SaveJson(entityData.Name);
                }
                ImGui.EndMenu();
            }
            if(ImGui.BeginMenu(""))
            {
                
                ImGui.EndMenu();
            }
            ImGui.EndMenuBar();
        }
    }
}
