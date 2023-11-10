namespace Engine.Systems.UI;
using ImGuiNET;
using System.Numerics;
using Raylib_cs;
using Engine.Data;

public class SettingsWindow : DocumentWindow
{
    SettingsStruct settingsStruct;
    
    public override void Setup()
    {
        settingsStruct = new();
    }

    public override void Show()
    {
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
        ImGui.SetNextWindowSizeConstraints(new Vector2(400, 400), new Vector2((float)Raylib.GetScreenWidth(), (float)Raylib.GetScreenHeight()));
        
        if (ImGui.Begin("Settings", ref Open, ImGuiWindowFlags.MenuBar))
        {
            DoMenu();
            Focused = ImGui.IsWindowFocused(ImGuiFocusedFlags.RootAndChildWindows);
            Vector2 size = ImGui.GetContentRegionAvail();
            Window();
            ImGui.End();
        }
        ImGui.PopStyleVar();
    }

    public override void Shutdown()
    {
        settingsStruct.SaveSettings();
    }

    private void Window()
    {
        if(ImGui.CollapsingHeader("Audio", ImGuiTreeNodeFlags.Framed))
        {
            ImGui.SliderFloat("Master Volume", ref settingsStruct.currentMasterVolume, 0f, 1f);
            if(ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.SetTooltip("Overall Audio Volume");
                ImGui.EndTooltip();
            }
            ImGui.SliderFloat("Music Volume", ref settingsStruct.currentMusicVolume, 0f, 1f);
            if(ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.SetTooltip("Music Volume");
                ImGui.EndTooltip();
            }
        }

        if(ImGui.CollapsingHeader("Video", ImGuiTreeNodeFlags.Framed))
        {
            ImGui.Checkbox("Fullscreen", ref settingsStruct.fullScreen);
            if(ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.SetTooltip("Turn fullscreen mode on or off");
                ImGui.EndTooltip();
            }
            ImGui.Checkbox("Custom window size", ref settingsStruct.windowResizable);
            if(ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.SetTooltip("Turn on or off the ability to resize the game window");
                ImGui.EndTooltip();
            }
            ImGui.InputInt("Target Framerate", ref settingsStruct.targetFPS);

        }
        if(ImGui.CollapsingHeader("Debug", ImGuiTreeNodeFlags.Framed))
        {
            ImGui.Checkbox("Logging", ref settingsStruct.logging);
            if(ImGui.IsItemHovered())
            {
                ImGui.BeginTooltip();
                ImGui.SetTooltip("Write log to Textfile");
                ImGui.EndTooltip();
            }
        }
    }

    private void DoMenu()
    {
        if(ImGui.BeginMenuBar())
        {
            if(ImGui.BeginMenu("File"))
            {
                if(ImGui.MenuItem(" - Save"))
                {
                    settingsStruct.SaveSettings();
                }
                ImGui.EndMenu();
            }
            ImGui.EndMenuBar();
        }
    }

    struct SettingsStruct
    {
        public SettingsStruct()
        {
            currentMasterVolume = Settings.audioSettings.MasterVolume;
            currentMusicVolume = Settings.audioSettings.MusicVolume;
            currentSFXVolume = Settings.audioSettings.SfxVolume;
            screenWidth = Settings.graphicsSettings.ScreenWidth;
            screenHeight = Settings.graphicsSettings.ScreenHeight;
            targetFPS = Settings.graphicsSettings.TargetFPS;
            splashScreenLength = Settings.cVars.SplashScreenLength;
            fullScreen = Settings.graphicsSettings.Fullscreen;
            //windowResizable = Engine.settings.windowResizable;
            logging = Settings.cVars.WriteTraceLog;
            vSync = Settings.graphicsSettings.VSync;
        }

        public float currentMasterVolume;
        public float currentMusicVolume;
        public float currentSFXVolume;
        public int screenWidth;
        public int screenHeight;
        public int targetFPS;
        public float splashScreenLength;
        public bool fullScreen;
        public bool logging;
        public bool windowResizable;
        public bool vSync;

        public void SaveSettings()
        {
            Settings.audioSettings.MasterVolume = currentMasterVolume;
            Settings.audioSettings.MusicVolume = currentMusicVolume;
            Settings.audioSettings.SfxVolume = currentSFXVolume;
            Settings.graphicsSettings.ScreenWidth = screenWidth;
            Settings.graphicsSettings.ScreenHeight = screenHeight;
            Settings.graphicsSettings.TargetFPS = targetFPS;
            Settings.cVars.SplashScreenLength = splashScreenLength;
            Settings.graphicsSettings.Fullscreen = fullScreen;
            //Engine.settings.windowResizable = windowResizable;
            Settings.cVars.WriteTraceLog = logging;
            Settings.graphicsSettings.VSync = vSync;

            Settings.SaveAllSettings();
        }
    }
}
