using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using ImGuiNET;

namespace DonutEngine.Backbone.Systems.UI;

public class SoundPlayer : DocumentWindow
{
    Vector2 buttonSize = new(100, 20);
    int currentPlaylistItem = 0;
    public override void Setup()
    {
        //throw new NotImplementedException();
    }

    public override void Show()
    {
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
        ImGui.SetNextWindowSizeConstraints(new Vector2(400, 400), new Vector2((float)Raylib.GetScreenWidth(), (float)Raylib.GetScreenHeight()));

        if (ImGui.Begin("Sound Player", ref Open, ImGuiWindowFlags.NoScrollbar))
        {
            Focused = ImGui.IsWindowFocused(ImGuiFocusedFlags.RootAndChildWindows);
            Vector2 size = ImGui.GetContentRegionAvail();
            ImGui.BeginChild("Music Control");
            {
                if(ImGui.Button("Play", buttonSize))
                {
                    DonutSystems.audioControl.ResumeMusic();
                }
                ImGui.SameLine();
                if(ImGui.Button("Pause/Play", buttonSize))
                {
                    DonutSystems.audioControl.PauseMusic();
                }
                ImGui.SameLine();
                ImGui.Text(DonutSystems.audioControl.CurrentMusicTime().ToString());
                ImGui.EndChild();
            }
            if(ImGui.BeginListBox("Playlist", new(200, 400)))
            {
                
                ImGui.ListBox("Playlist", ref currentPlaylistItem, DonutSystems.audioControl.GetPlaylist(), DonutSystems.audioControl.GetPlaylistLength());
                ImGui.EndListBox();
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
}