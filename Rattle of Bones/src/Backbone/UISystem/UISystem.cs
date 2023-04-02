using DonutEngine.Backbone.Systems.UI;
using Raylib_cs;
using ImGuiNET;
namespace DonutEngine.Backbone.Systems;

public class UISystem : SystemsClass
{
    public override void DrawUpdate()
    {
        rlImGui.Begin();
        ImGui.ShowDemoWindow();
        rlImGui.End();   
    }

    public override void LateUpdate()
    {
        
    }

    public override void Shutdown()
    {
        rlImGui.Shutdown();
    }

    public override void Start()
    {
        System.Console.WriteLine("Started UI sys");
        rlImGui.Setup(true);
    }

    public override void Update()
    {
        
    }
}

