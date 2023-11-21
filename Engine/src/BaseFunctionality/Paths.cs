namespace Engine;
/// <summary>
/// Here is shorthand names from strings for the different resources the engine uses.
/// </summary>
public static class Paths
{
    public readonly static string app =  AppDomain.CurrentDomain.BaseDirectory;
    public static string GraphicsSettings { get { return AppDomain.CurrentDomain.BaseDirectory+"GraphicsSettings.json"; } }
    public static string AudioSettings { get { return AppDomain.CurrentDomain.BaseDirectory+"AudioSettings.json"; } }
    public static string InputSettings { get { return AppDomain.CurrentDomain.BaseDirectory+"InputSettings.json"; } }
    public static string SpritesPath 
    {
        get
        {
            #if OS_WINDOWS
                return app+"Data\\Sprites\\";
            #elif OS_LINUX
                return app+"Data/Sprites/"; 
            #endif
        }
    }

    public static string SfxPath 
    {
        get
        {
            #if OS_WINDOWS
                return app+"Data\\Sound\\SFX\\";
            #elif OS_LINUX 
                return app+"Data/Sound/SFX/";
            #endif
        }
    }

    public static string MusicPath 
    {
        get
        {
            #if OS_WINDOWS
                return app+"Data\\Sound\\Music\\";
            #elif OS_LINUX
                return app+"Data/Sound/Music/";
            #endif
        }
    }

    public static string AudioDefPath 
    {
        get
        {
            #if OS_WINDOWS
                return app+"Data\\Sound\\";
            #elif OS_LINUX
                return app+"Data/Sound/";
            #endif
        }
    }

    public static string MeshPath 
    {
        get
        {
            #if OS_WINDOWS
                return app+"Data\\Mesh\\";
            #elif OS_LINUX
                return app+"Data/Mesh/";
            #endif 
        }
    }

    public static string ScenesPath 
    {
        get
        {
            #if OS_WINDOWS
                return app+"Data\\Scenes\\";
            #elif OS_LINUX
                return app+"Data/Scenes/";
            #endif 
        }
    }

    public static string TexturesPath 
    {
        get
        {
            #if OS_WINDOWS
                return app+"Data\\Textures\\";
            #elif OS_LINUX
                return app+"Data/Textures/";
            #endif 
        }
    }

    public static string FontPath 
    {
        get
        {
            #if OS_WINDOWS
                return app+"Data\\Font\\";
            #elif OS_LINUX
                return app+"Data/Font/"; 
            #endif
        }
    }

    public static string ShaderPath 
    {
        get
        {
            #if OS_WINDOWS
                return app+"Data\\Shaders\\";
            #elif OS_LINUX
                return app+"Data/Shaders/";
            #endif 
        }
    }

    public static string BSPPath 
    {
        get
        {
            #if OS_WINDOWS
                return app+"Data\\Bsp\\";
            #elif OS_LINUX
                return app+"Data/Bsp/";
            #endif 
        }
    }
}
