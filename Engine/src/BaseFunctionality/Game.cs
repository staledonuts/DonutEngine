using Raylib_cs;
using static Raylib_cs.Raylib;
using Engine.Systems;
using Engine.Systems.Input;
using Engine.Logging;
using Engine.Assets;
using Engine.Systems.SceneSystem;
using Engine.Systems.UI.Skeleton;
using Engine.Utils;
using Engine;
using System.Numerics;

public class Game
{
    readonly string windowName;
    protected static Shader shader;
    protected static RenderTexture2D target;
    public Game(string gamename)
    {
        windowName = gamename;
    }
    public virtual void InitGame(string[] args)
    {
        Settings.CheckSettings();
        EngineArgParser.ArgInput(args);
        App.Initialize();
        InputEventSystem.Initialize();
        InitSystems();
        shader = Raylib.LoadShader(null ,Paths.ShaderPath+"glsl330/bloom.fs");
        target = Raylib.LoadRenderTexture(Raylib.GetScreenWidth(), Raylib.GetScreenHeight());
        SetupGame();
        SetWindowTitle(windowName);
        MainLoop();
        ShutdownEngine();
    }

    private void ShutdownEngine()
    {
        EngineSystems.ShutdownSystems();
        CloseWindow();
    }

    private void InitSystems()
    {
        Textures.InitTextureLibrary();
        Fonts.InitFontLibrary();
        EngineSystems.AddSystem(new AudioControl());
        EngineSystems.AddSystem(new SceneManager());
        EngineSystems.AddSystem(new Camera2DSystem());
        //EngineSystems.AddSystem(new SkeletonUISystem(new Style(Color.BLANK, Color.BLANK, Color.GRAY, Color.DARKBLUE, Color.DARKGRAY, Color.GRAY, Color.SKYBLUE, Color.DARKGRAY, Fonts.GetFont("PixelOperator"), 24, 1)));
    }

    /// <summary>
    /// This is where you create and initialize all your systems and scenes <see cref="Point"/>.
    /// </summary>
    public virtual void SetupGame()
    {
        //override me.
    }

    /// <summary>
    /// Main Loop of the Engine.
    /// The idea of there updaters is that you should be using Update() for game logic, DrawUpdate() for drawing and LateUpdate() for things like physics calculation and non-gameplay affecting systems.
    /// </summary>
    void MainLoop()
    {
        while (!WindowShouldClose())
        {
            //App.UpdateApp();
            //Physics, Player logic and so forth. Before draw.
            UpdateLoop();
            //All draw logic.
            DrawUpdate();
            //Camera and less priority nesseccary things go here.
            LateUpdate();
        }

    }
    static void UpdateLoop()
    {
        EngineSystems.Update();
    }

    static void DrawUpdate()
    {
        BeginDrawing();
        BeginTextureMode(target);
        Backgrounds.DrawBackground();
        EngineSystems.DrawUpdate();
        EndTextureMode();
        BeginShaderMode(shader);
        DrawTextureRec(target.Texture, new Rectangle(0, 0, target.Texture.Width, -target.Texture.Height), new Vector2(0, 0), Color.White);
        EndShaderMode();
        EndDrawing();
    }

    static void LateUpdate()
    {
        EngineSystems.LateUpdate();
    }
}

