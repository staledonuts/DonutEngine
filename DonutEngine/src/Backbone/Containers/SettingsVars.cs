namespace DonutEngine.Backbone.Systems;
using IniParser.Model;
using IniParser;
using Raylib_cs;
using DonutEngine;
public class SettingsVars
{
    //Contains various settings for the game engine.
    public SettingsVars(string settingsPath)
    {
        parser = new FileIniDataParser();
        try
        {
            Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ Trying to Load Settings ]------");
            LoadSettings(settingsPath);
        }
        catch
        {
            Raylib.TraceLog(TraceLogLevel.LOG_WARNING, "------[ There was a Settings Error: Creating Default Fallback ]------");
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
    public bool logging { get; set; }


    public string spritesPath { get; set; }
    public string sfxPath { get; set; }
    public string musicPath { get; set; }
    public string audioDefPath { get; set; }
    public string entitiesPath { get; set; }
    public string texturesPath { get; set; }

    public void SetDefaultSettings()
    {
        currentMasterVolume = 1;
        currentMusicVolume = 0.5f;
        currentSFXVolume = 0.8f;
        screenWidth = 800;
        screenHeight = 600;
        targetFPS = 60;
        splashScreenLength = 60;
        fullScreen = false;
        logging = false;
        #if WIN64
        spritesPath = "Resources\\Sprites\\";
        sfxPath = "Resources\\Sound\\SFX\\";
        musicPath = "Resources\\Sound\\Music\\";
        audioDefPath = "Resources\\Scripts\\Sound\\SoundFileDef.ini";
        entitiesPath = "Resources\\Scripts\\Entities\\";
        texturesPath = "Resources\\Textures\\";
        #else
        spritesPath = "Resources/Sprites/";
        sfxPath = "Resources/Sound/SFX/";
        musicPath = "Resources/Sound/Music/";
        audioDefPath = "Resources/Scripts/Sound/SoundFileDef.ini";
        entitiesPath = "Resources/Scripts/Entities/";
        texturesPath = "Resources/Textures/";
        #endif

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
        data["Settings"]["logging"] = logging.ToString();
        data["FilePaths"]["spritesPath"] = spritesPath;
        data["FilePaths"]["sfxPath"] = sfxPath;
        data["FilePaths"]["musicPath"] = musicPath;
        data["FilePaths"]["audioDefPath"] = audioDefPath;
        data["FilePaths"]["entitiesPath"] = entitiesPath;
        data["FilePaths"]["texturesPath"] = texturesPath;
        parser.WriteFile(settingsPath, data);
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ Settings Saved ]------");
    } 

    public void LoadSettings(string settingsPath)
    {
        IniData data = parser.ReadFile(settingsPath);
        //Settings Loading
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
        logging = bool.Parse(sectionData.Keys.GetKeyData("logging").Value);

        //FilePaths Loading
        sectionData = data.Sections.GetSectionData("FilePaths");
        spritesPath = sectionData.Keys.GetKeyData("spritesPath").Value;
        sfxPath = sectionData.Keys.GetKeyData("sfxPath").Value;
        musicPath = sectionData.Keys.GetKeyData("musicPath").Value;
        audioDefPath = sectionData.Keys.GetKeyData("audioDefPath").Value;
        entitiesPath = sectionData.Keys.GetKeyData("entitiesPath").Value;
        texturesPath = sectionData.Keys.GetKeyData("texturesPath").Value;
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "------[ Settings Loaded ]------");
    }   
}
