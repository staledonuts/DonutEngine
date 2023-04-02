namespace DonutEngine.Backbone.Systems;
using IniParser.Model;
using IniParser;
using Raylib_cs;
public class SettingsVars
{
    //Contains various settings for the game engine.
    public SettingsVars(string settingsPath)
    {
        parser = new FileIniDataParser();
        try
        {
            LoadSettings(settingsPath);
        }
        catch
        {
            System.Console.WriteLine("WARNING: ---[There was a Settings Error: Creating Default Fallback]---");
            SetDefaultSettings();
            SaveSettings(settingsPath);
        }
        
        
    }
    FileIniDataParser parser;
    public float currentMasterVolume { get; set; }
    public float currentMusicVolume { get; set; }
    public float currentSFXVolume { get; set; }
    public int screenWidth { get; set; }
    public int screenHeight { get; set; }
    public readonly int virtualScreenWidth = 320;
    public readonly int virtualScreenHeight = 160;
    public int targetFPS { get; set; }
    public int splashScreenLength { get; set; }
    public bool fullScreen { get; set; }

    public void SetDefaultSettings()
    {
        currentMasterVolume = 1;
        currentMusicVolume = 0.5f;
        currentSFXVolume = 0.8f;
        screenWidth = 800;
        screenHeight = 600;
        targetFPS = 60;
        splashScreenLength = 120;
        fullScreen = false;
    }
    public void SaveSettings(string settingsPath)
    {
        IniData data = new();
        data["Settings"]["currentMasterVolume"] = currentMasterVolume.ToString();
        data["Settings"]["currentMusicVolume"] = currentMusicVolume.ToString();
        data["Settings"]["currentSFXVolume"] = currentSFXVolume.ToString();
        data["Settings"]["screenWidth"] = screenWidth.ToString();
        data["Settings"]["screenHeight"] = screenHeight.ToString();
        data["Settings"]["targetFPS"] = targetFPS.ToString();
        data["Settings"]["splashScreenLength"] = splashScreenLength.ToString();
        data["Settings"]["fullScreen"] = fullScreen.ToString();
        parser.WriteFile(settingsPath, data);
    } 

    public void LoadSettings(string settingsPath)
    {
        IniData data = parser.ReadFile(settingsPath);
        SectionData sectionData = data.Sections.GetSectionData("Settings");
        currentMasterVolume = float.Parse(sectionData.Keys.GetKeyData("currentMasterVolume").Value);
        Raylib.SetMasterVolume(currentMasterVolume);
        currentMusicVolume = float.Parse(sectionData.Keys.GetKeyData("currentMusicVolume").Value);
        currentSFXVolume = float.Parse(sectionData.Keys.GetKeyData("currentSFXVolume").Value);
        screenWidth = int.Parse(sectionData.Keys.GetKeyData("screenWidth").Value);
        screenHeight = int.Parse(sectionData.Keys.GetKeyData("screenHeight").Value);
        targetFPS = int.Parse(sectionData.Keys.GetKeyData("targetFPS").Value);
        splashScreenLength = int.Parse(sectionData.Keys.GetKeyData("splashScreenLength").Value);
        fullScreen = bool.Parse(sectionData.Keys.GetKeyData("fullScreen").Value);
        System.Console.WriteLine("INFO: ---[Settings Loaded]---");
    }   
}
