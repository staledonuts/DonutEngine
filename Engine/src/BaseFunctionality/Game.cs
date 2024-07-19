using Engine.Systems;
using Engine.Systems.EngineInput;
using Engine.Logging;
using Engine.Assets;
using Engine.Systems.SceneSystem;
using Engine.Systems.UI.Skeleton;
using Engine.Utils;
using Engine;
using System.Numerics;
using Engine.RenderSystems;
using Raylib_CSharp.Windowing;

public class Game
{
    readonly string windowName;
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
        SetupGame();
        Window.SetTitle(windowName);
        MainLoop();
        ShutdownEngine();
    }

    private void ShutdownEngine()
    {
        EngineSystems.ShutdownSystems();
        Window.Close();
    }

    private void InitSystems()
    {
        Textures.Initialize();
        Fonts.Initialize();
        ShaderLib.Initialize();
        EngineSystems.AddSystem(new AudioControl());
        EngineSystems.AddSystem(new SceneManager());
        //EngineSystems.AddSystem(new SkeletonUISystem(new Style(Color.Blank, Color.Blank, Color.Gray, Color.DarkBlue, Color.DarkGray, Color.Gray, Color.SkyBlue, Color.DarkGray, Fonts.GetFont("PixelOperator"), 24, 1)));
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
        while (!Window.ShouldClose())
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
        InputEventSystem.UpdateInputEvent();
        EngineSystems.Update();
    }

    static void DrawUpdate()
    {
        EngineSystems.DrawUpdate();
    }

    static void LateUpdate()
    {
        EngineSystems.LateUpdate();
        EngineSystems.MiscUpdate();
    }
}

