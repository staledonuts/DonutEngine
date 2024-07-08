using System.Numerics;
using Engine.Assets;
using Raylib_CSharp.Windowing;

namespace Engine
{
    using System.IO;
    using Engine.Systems.Containers;
    using Newtonsoft.Json;
    using Raylib_CSharp.Logging;

    public static class Settings
    {
        public static GraphicsSettings graphicsSettings = new();
        public static InputSettings inputSettings = new();
        public static AudioSettings audioSettings = new();
        public static CVars cVars = new();

        public static void CheckSettings()
        {
            try
            {
                Logger.TraceLog(TraceLogLevel.Info, "------[ Trying to Load Settings ]------");
                LoadSettings();
            }
            catch
            {
                Logger.TraceLog(TraceLogLevel.Warning, "------[ There was a Settings Error: Creating Default Fallback ]------");
                SetDefaultSettings(DefaultsPicker.AllDefaults);
            }  
        }

        public static void SaveAllSettings()
        {
            SaveGraphicsSettings();
            SaveAudioSettings();
            SaveInputSettings();

        }

        public static void SaveGraphicsSettings()
        {
            string jsonString = JsonConvert.SerializeObject(graphicsSettings, Formatting.Indented);
            File.WriteAllText(Paths.GraphicsSettings, jsonString);
        }

        public static void SaveInputSettings()
        {
            string jsonString = JsonConvert.SerializeObject(inputSettings, Formatting.Indented);
            File.WriteAllText(Paths.InputSettings, jsonString);
        }

        public static void SaveAudioSettings()
        {
            string jsonString = JsonConvert.SerializeObject(audioSettings, Formatting.Indented);
            File.WriteAllText(Paths.AudioSettings, jsonString);
        }

#nullable disable
        public static void LoadSettings()
        {
            string jsonData = File.ReadAllText(Paths.GraphicsSettings);
            graphicsSettings = JsonConvert.DeserializeObject<GraphicsSettings>(jsonData);
            jsonData = File.ReadAllText(Paths.InputSettings);
            inputSettings = JsonConvert.DeserializeObject<InputSettings>(jsonData);
            jsonData = File.ReadAllText(Paths.AudioSettings);
            audioSettings = JsonConvert.DeserializeObject<AudioSettings>(jsonData);
        }
#nullable enable

        

        public static bool ToggleFullScreen()
        {
            /*if(GlobalData.Graphics.IsFullScreen)
            {
                GlobalData.Graphics.ToggleFullScreen();
                GlobalData.Graphics.PreferredBackBufferHeight = graphicsSettings.ScreenHeight;
                GlobalData.Graphics.PreferredBackBufferWidth = graphicsSettings.ScreenWidth;
                GlobalData.Graphics.ApplyChanges();
                return false;
            }
            else
            {
                GlobalData.Graphics.PreferredBackBufferHeight = graphicsSettings.FullscreenHeight;
                GlobalData.Graphics.PreferredBackBufferWidth = graphicsSettings.FullscreenWidth;
                GlobalData.Graphics.ApplyChanges();
                GlobalData.Graphics.ToggleFullScreen();
            }*/
                return true;
        }
        

        public static void SetDefaultSettings(DefaultsPicker choose)
        {
            switch (choose)
            {
                case DefaultsPicker.GraphicsDefault:
                    graphicsSettings = new();
                    SaveGraphicsSettings();
                    break;
                case DefaultsPicker.InputDefault:
                    inputSettings = new();
                    SaveInputSettings();
                    break;
                case DefaultsPicker.AudioDefault:
                    audioSettings = new();
                    SaveAudioSettings();
                    break;
                case DefaultsPicker.AllDefaults:
                    graphicsSettings = new();
                    inputSettings = new();
                    audioSettings = new();
                    SaveAllSettings();
                    break;
            }
        }

    }
}

namespace Engine.Systems.Containers
{
    public class GraphicsSettings
    {
        public GraphicsSettings()
        {
            MaximumParticles = 10;
            ScreenWidth = 1024;
            ScreenHeight = 768;
            TargetFPS = 60;
            VirtualScreenHeight = 384;
            VirtualScreenWidth = 512;
            Fullscreen = false;
            VSync = true;
        }

        public int MaximumParticles { get; set; }
        public int ScreenWidth { get; set; }
        public int ScreenHeight { get; set; }
        public int TargetFPS { get; set; }
        public int FullscreenWidth { get { return Window.GetMonitorPhysicalWidth(Window.GetCurrentMonitor()); } }
        public int FullscreenHeight { get { return Window.GetMonitorPhysicalHeight(Window.GetCurrentMonitor()); } }
        public int VirtualScreenWidth { get; set; }
        public int VirtualScreenHeight { get; set; }
        public bool Fullscreen { get; set; }
        public bool VSync { get; set; }
        public bool WindowResizable { get { return false; } }

        public static void ApplyGraphicsSettings()
        {
            /*GlobalData.Graphics.PreferredBackBufferHeight = graphicsSettings.ScreenHeight;
            GlobalData.Graphics.PreferredBackBufferWidth = graphicsSettings.ScreenWidth;
            GlobalData.Graphics.ApplyChanges();*/
        }
       
    }

    public class InputSettings
    {
        
    }

    public class AudioSettings
    {
        public AudioSettings()
        {
            MasterVolume = 0.5f;
            SfxVolume = 0.5f;
            MusicVolume = 0.5f;
            VoiceVolume = 0.5f;
        }
        public float MasterVolume { get; set; }
        public float SfxVolume { get; set; }
        public float MusicVolume { get; set; }
        public float VoiceVolume { get; set; }

        public void ApplyAudioSettings()
        {
            EngineSystems.GetSystem<AudioControl>().SetMasterVolume();
        }
    }

    public class CVars
    {
        public CVars()
        {
            WriteTraceLog = false;
            SplashScreenLength = 2f;
            Windowname = "DonutEngine";
            Debugging = true;
            Focused = true;
        }

        public bool WriteTraceLog { get; set; }
        public float SplashScreenLength { get; set; }
        public bool Debugging { get; set; }
        public string Windowname { get; set; }
        public bool Focused { get; set; }

    }

    public enum DefaultsPicker
    {
        GraphicsDefault,
        InputDefault,
        AudioDefault,
        AllDefaults,
    }


}