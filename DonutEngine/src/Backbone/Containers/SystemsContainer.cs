namespace DonutEngine;
using DonutEngine.Backbone.Systems.Audio;
using DonutEngine.Backbone.Systems.Window;
using DonutEngine.Backbone.Systems.UI;
using DonutEngine.Backbone.Systems.Physics;
using DonutEngine.Backbone.Systems;
using DonutEngine.Backbone;
using DonutEngine.Backbone.Textures;
public static class Sys
{
    public static readonly SettingsVars settingsVars = new();
    public static readonly WindowSystem windowSystem = new();
    public static readonly TextureContainer textureContainer = new();
    public static readonly UISystem uISystem = new();
    public static readonly AudioControl audioControl = new();
    public static readonly PhysicsSystem physicsSystem = new();
    public static readonly EntityManager entityManager = new();
    public static readonly LevelDataSystem levelDataSystem = new();
    public static CameraHandler cameraHandler = new();
}