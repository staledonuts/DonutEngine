using System;
using System.Collections.Generic;
using System.Numerics;
using Raylib_cs;
using ImGuiNET;

namespace DonutEngine.Backbone.Systems.UI;

public class SoundPlayer : DocumentWindow
{
    Vector2 buttonSize = new(100, 18);
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

    public override void DrawUpdate()
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
                
                if(ImGui.MenuItem("Reload Audio Library"))
                {
                    Sys.audioControl.ReloadAudioLibrary();
                }
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
                string[] playlist = Sys.audioControl.GetMusicPlaylist();
                string currentSelection = playlist.GetValue(currentMusicPlaylistItem).ToString();
                Sys.audioControl.StopMusic();
                Sys.audioControl.PlayMusic(currentSelection);
            }
            ImGui.SameLine();
            if(ImGui.Button("Pause/Resume", buttonSize))
            {
                Sys.audioControl.PauseMusic();
            }
            ImGui.SameLine();
            ImGui.Text(Sys.audioControl.CurrentMusicTime().ToString());
            ImGui.SameLine();
            ImGui.Text(" / "+Sys.audioControl.CurrentMusicLength().ToString());
            ImGui.NewLine();
            ImGui.BeginListBox("Muzak", width);
            ImGui.ListBox("Music Playlist", ref currentMusicPlaylistItem, Sys.audioControl.GetMusicPlaylist(), Sys.audioControl.GetMusicPlaylistLength());
            ImGui.EndListBox();
            ImGui.EndTabItem();
        }
        if(ImGui.BeginTabItem("SFX Tester"))
        {
            if(ImGui.Button("PlaySFX", buttonSize))
            {
                string[] playlist = Sys.audioControl.GetSFXPlaylist();
                string currentSelection = playlist.GetValue(currentSFXPlaylistItem).ToString();
                Sys.audioControl.PlaySFX(currentSelection);
            }
            ImGui.NewLine();
            ImGui.BeginListBox("SFX", width);
            ImGui.ListBox("SFX Playlist", ref currentSFXPlaylistItem, Sys.audioControl.GetSFXPlaylist(), Sys.audioControl.GetSFXPlaylistLength(), Sys.audioControl.GetSFXPlaylistLength());
            ImGui.EndListBox();
            ImGui.NewLine();
            ImGui.EndTabItem();
        }       
    }
}