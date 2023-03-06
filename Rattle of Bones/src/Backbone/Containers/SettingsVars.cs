namespace DonutEngine.Backbone.Systems;
using IniParser.Model;
using IniParser;
public class SettingsVars
{
    
    public SettingsVars()
    {
        FileIniDataParser parser = new FileIniDataParser();
        IniData data = parser.ReadFile(DonutFilePaths.settings+"Settings.ini");
        SectionData sectionData = data.Sections.GetSectionData("Settings");
        currentMasterVolume = float.Parse(sectionData.Keys.GetKeyData("currentMasterVolume").Value);
        currentMusicVolume = float.Parse(sectionData.Keys.GetKeyData("currentMusicVolume").Value);
        currentSFXVolume = float.Parse(sectionData.Keys.GetKeyData("currentSFXVolume").Value);
        screenWidth = int.Parse(sectionData.Keys.GetKeyData("screenWidth").Value);
        screenHeight = int.Parse(sectionData.Keys.GetKeyData("screenHeight").Value);
        splashScreenLength = int.Parse(sectionData.Keys.GetKeyData("splashScreenLength").Value);
        fullScreen = bool.Parse(sectionData.Keys.GetKeyData("fullScreen").Value);
    }
    public float currentMasterVolume { get; set; }
    public float currentMusicVolume { get; set; }
    public float currentSFXVolume { get; set; }
    public int screenWidth { get; set; }
    public int screenHeight { get; set; }
    public int splashScreenLength { get; set; }
    public bool fullScreen { get; set; }
}
