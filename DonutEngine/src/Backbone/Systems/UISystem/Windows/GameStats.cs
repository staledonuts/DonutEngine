#if DEBUG
using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using ImGuiNET;

namespace DonutEngine.Backbone.Systems.UI;
public class GameStats : DocumentWindow
{

    public override void Setup()
    {

    }

    public override void Show()
    {
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
        ImGui.SetNextWindowSizeConstraints(new Vector2(400, 400), new Vector2((float)Raylib.GetScreenWidth(), (float)Raylib.GetScreenHeight()));

        if (ImGui.Begin("GameStats", ref Open, ImGuiWindowFlags.NoScrollbar))
        {
            Focused = ImGui.IsWindowFocused(ImGuiFocusedFlags.RootAndChildWindows);

            Vector2 size = ImGui.GetContentRegionAvail();

            if (ImGui.BeginTabBar("Toolbar", ImGuiTabBarFlags.Reorderable | ImGuiTabBarFlags.Reorderable))
            {
                if(ImGui.BeginTabItem("Physics"))
                {
                   ImGui.TextUnformatted(String.Format("Current Physics Bodies: "+Sys.physicsSystem.physicsInfo.currentPhysicsBodies.ToString()));
                   ImGui.EndTabItem();
                }
                if(ImGui.BeginTabItem("Camera"))
                {
                    GameCamera2D tempEntity = Sys.cameraHandler.returnCurrentCamTarget();
                    ImGui.TextUnformatted(String.Format("Current Camera Target: "+tempEntity.ToString()));
                    //ImGui.TextUnformatted(String.Format("Current Target Pos: "+tempEntity.position.ToString()));
                    ImGui.TextUnformatted(String.Format("Current Camera Pos: "+Sys.cameraHandler.donutcam.target));
                    ImGui.TextUnformatted(String.Format("Current Camera Modes: "+Sys.cameraHandler.returnCameraModes().ToString()));
                    ImGui.EndTabItem();
                }
                ImGui.EndChild();
            }
            ImGui.End();
        }
        ImGui.PopStyleVar();
    }

    public override void Shutdown()
    {
        
    }

    public override void DrawUpdate()
    {
        if (!Open)
            return;
            
    }

}
#endif