using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using ImGuiNET;

namespace DonutEngine.Backbone.Systems.UI;

public class LogWindow : DocumentWindow
{
    Vector2 buttonSize = new(100, 20);
    public override void Setup()
    {
        Raylib.SetTraceLogLevel(TraceLogLevel.LOG_ALL);
    }

    public override void Show()
    {
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
        ImGui.SetNextWindowSizeConstraints(new Vector2(400, 400), new Vector2((float)Raylib.GetScreenWidth(), (float)Raylib.GetScreenHeight()));
        if (ImGui.Begin("Log", ref Open, ImGuiWindowFlags.NoScrollbar))
        {
            Focused = ImGui.IsWindowFocused(ImGuiFocusedFlags.RootAndChildWindows);
            Vector2 size = ImGui.GetContentRegionAvail();
            Vector2 width = new(ImGui.GetColumnWidth(), ImGui.GetContentRegionAvail().Y);
            if(ImGui.BeginTabBar("LogWindowTabs"))
            {
                if(ImGui.BeginTabItem("Log"))
                {
                    
                    ImGui.EndTabItem();
                }
                ImGui.EndTabBar();
            }
            ImGui.End();
        }
        ImGui.PopStyleVar();
    }
    public override void Shutdown()
    {
        //throw new NotImplementedException();
    }

    public override void Update()
    {
        if (!Open)
        {
            return;
        }
    }

    private void DoMenu()
    {
        if(ImGui.BeginMenuBar())
        {
            if(ImGui.BeginMenu("File"))
            {
                if(ImGui.MenuItem("Reload Entities", "F5"))
                {
                    DonutSystems.entityManager.ReloadEntities();
                }
                if(ImGui.MenuItem("Save As"))
                {
                    
                }
                ImGui.EndMenu();
            }
            ImGui.EndMenuBar();
        }
    }
}