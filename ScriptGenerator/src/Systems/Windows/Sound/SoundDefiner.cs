namespace DonutEngine.Backbone.Systems.UI.Audio;
using Raylib_cs;
using ImGuiNET;
using Newtonsoft.Json;
using System.Numerics;
public class SoundDefiner : DocumentWindow
{
    SoundDef soundDef = new();
    string Filename;
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
                    ReloadWindow();
                }
                if(ImGui.MenuItem(" [Save As]"))
                {
                    SaveJson(Filename);
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

    private void SaveJson(string json)
    {
        if(json == "")
        {
            return;
        }
        else
        {
            var settings = new JsonSerializerSettings{Formatting = Formatting.Indented, NullValueHandling = NullValueHandling.Ignore};
            var data = JsonConvert.SerializeObject(new {soundDef}, settings);

            File.WriteAllText(DonutFilePaths.app+Sys.settingsVars.audioDefPath+json+".json", data);
        }
    }

    private void ReloadWindow()
    {

    }
    
    private struct SoundDef
    {

    }

    private void EntityWindow()
    {
        if(Filename == null)
        {
            Filename = "";
        }
        ImGui.InputTextWithHint("Entity Filename", "Input Filename: will be saved as a Json", ref Filename, 32, ImGuiInputTextFlags.AutoSelectAll);
        
        
        
    }
}
