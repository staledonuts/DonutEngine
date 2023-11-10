namespace Engine;

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
            if(OperatingSystem.IsWindows())
            {
                return app+"Data\\Sprites\\";
            }
            else
            {
                return app+"Data/Sprites/";
            } 
        }
    }

    public static string SfxPath 
    {
        get
        {
            if(OperatingSystem.IsWindows())
            {
                return app+"Data\\Sound\\SFX\\";
            }
            else
            {
                return app+"Data/Sound/SFX/";
            } 
        }
    }

    public static string MusicPath 
    {
        get
        {
            if(OperatingSystem.IsWindows())
            {
                return app+"Data\\Sound\\Music\\";
            }
            else
            {
                return app+"Data/Sound/Music/";
            } 
        }
    }

    public static string AudioDefPath 
    {
        get
        {
            if(OperatingSystem.IsWindows())
            {
                return app+"Data\\Sound\\";
            }
            else
            {
                return app+"Data/Sound/";
            } 
        }
    }

    public static string MeshPath 
    {
        get
        {
            if(OperatingSystem.IsWindows())
            {
                return app+"Data\\Mesh\\";
            }
            else
            {
                return app+"Data/Mesh/";
            } 
        }
    }

    public static string ScenesPath 
    {
        get
        {
            if(OperatingSystem.IsWindows())
            {
                return app+"Data\\Scenes\\";
            }
            else
            {
                return app+"Data/Scenes/";
            } 
        }
    }

    public static string TexturesPath 
    {
        get
        {
            if(OperatingSystem.IsWindows())
            {
                return app+"Data\\Textures\\";
            }
            else
            {
                return app+"Data/Textures/";
            } 
        }
    }

    public static string FontPath 
    {
        get
        {
            if(OperatingSystem.IsWindows())
            {
                return app+"Data\\Font\\";
            }
            else
            {
                return app+"Data/Font/";
            } 
        }
    }

    public static string ShaderPath 
    {
        get
        {
            if(OperatingSystem.IsWindows())
            {
                return app+"Data\\Shaders\\";
            }
            else
            {
                return app+"Data/Shaders/";
            } 
        }
    }

    public static string BSPPath 
    {
        get
        {
            if(OperatingSystem.IsWindows())
            {
                return app+"Data\\Bsp\\";
            }
            else
            {
                return app+"Data/Bsp/";
            } 
        }
    }
}
