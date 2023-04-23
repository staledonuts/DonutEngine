namespace DonutEngine;
using DonutEngine.Backbone.Systems.UI;
using DonutEngine.Backbone.Systems;
using DonutEngine.Backbone.Textures;
public static class Sys
{
    public static readonly SettingsVars settingsVars = new();
    public static readonly WindowSystem windowSystem = new();
    public static readonly TextureContainer textureContainer = new();
    public static readonly UISystem uISystem = new();
}