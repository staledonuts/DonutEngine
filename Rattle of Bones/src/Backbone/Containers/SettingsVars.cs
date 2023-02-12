namespace DonutEngine.Backbone.Systems;

public class SettingsVars
{
    public SettingsVars()
    {
        currentMasterVolume = 1f;
        currentMusicVolume = 1f;
        currentSFXVolume = 0.75f;
        screenWidth = 800;
        screenHeight = 450;
    }
    public float currentMasterVolume;
    public float currentMusicVolume;
    public float currentSFXVolume;

    public readonly int screenWidth = 800;
    public readonly int screenHeight = 450;
}
