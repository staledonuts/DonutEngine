using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using ImGuiNET;

namespace DonutEngine.Backbone.Systems.UI;

public class SoundPlayer : DocumentWindow
{
    Vector2 buttonSize = new(100, 20);
    int currentMusicPlaylistItem = 0;
    int currentSFXPlaylistItem = 0;
    public override void Setup()
    {
        //throw new NotImplementedException();
    }

    public override void Show()
    {
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
        ImGui.SetNextWindowSizeConstraints(new Vector2(400, 400), new Vector2((float)Raylib.GetScreenWidth(), (float)Raylib.GetScreenHeight()));

        if (ImGui.Begin("Sound Player", ref Open, ImGuiWindowFlags.MenuBar | ImGuiWindowFlags.NoScrollbar))
        {
            Focused = ImGui.IsWindowFocused(ImGuiFocusedFlags.RootAndChildWindows);
            Vector2 size = ImGui.GetContentRegionAvail();
            Vector2 width = ImGui.GetContentRegionMax();
            DoMenu();
            if(ImGui.BeginTabBar("Sound Tester"))
            {
                DoContent(size);
                ImGui.EndTabBar();
            }
            ImGui.End();
        }
        ImGui.PopStyleVar();
    }
    public override void Shutdown()
    {
        
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
            if(ImGui.BeginMenu("Debug"))
            { 
                
                
                ImGui.EndMenu();
            }
            ImGui.EndMenuBar();
        }
    }

    private void DoContent(Vector2 width)
    {
        if(ImGui.BeginTabItem("Music Player"))
        {
            if(ImGui.Button("Play", buttonSize))
            {
                string[] playlist = DonutSystems.audioControl.GetMusicPlaylist();
                string currentSelection = playlist.GetValue(currentMusicPlaylistItem).ToString();
                DonutSystems.audioControl.StopMusic();
                DonutSystems.audioControl.PlayMusic(currentSelection);
            }
            ImGui.SameLine();
            if(ImGui.Button("Pause/Resume", buttonSize))
            {
                DonutSystems.audioControl.PauseMusic();
            }
            ImGui.SameLine();
            if(ImGui.Button("Reload Music", buttonSize))
            {
                
            }
            ImGui.SameLine();
            ImGui.Text(DonutSystems.audioControl.CurrentMusicTime().ToString());
            ImGui.SameLine();
            ImGui.Text(" / "+DonutSystems.audioControl.CurrentMusicLength().ToString());
            ImGui.NewLine();
            ImGui.BeginListBox("Muzak", width);
            ImGui.ListBox("Music Playlist", ref currentMusicPlaylistItem, DonutSystems.audioControl.GetMusicPlaylist(), DonutSystems.audioControl.GetMusicPlaylistLength());
            ImGui.EndListBox();
            ImGui.EndTabItem();
        }
        if(ImGui.BeginTabItem("SFX Tester"))
        {
            if(ImGui.Button("PlaySFX", buttonSize))
            {
                string[] playlist = DonutSystems.audioControl.GetSFXPlaylist();
                string currentSelection = playlist.GetValue(currentSFXPlaylistItem).ToString();
                DonutSystems.audioControl.PlaySFX(currentSelection);
            }
            ImGui.SameLine();
            if(ImGui.Button("Reload Sounds", buttonSize))
            {
                
            }
            ImGui.NewLine();
            ImGui.BeginListBox("SFX", width);
            ImGui.ListBox("SFX Playlist", ref currentSFXPlaylistItem, DonutSystems.audioControl.GetSFXPlaylist(), DonutSystems.audioControl.GetSFXPlaylistLength(), DonutSystems.audioControl.GetSFXPlaylistLength());
            ImGui.EndListBox();
            ImGui.NewLine();
            ImGui.EndTabItem();
        }       

        

    }
}