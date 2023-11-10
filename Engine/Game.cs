using Engine;
using Engine.Data;
using Engine.Systems;
using Engine.Systems.Input;
using static Raylib_cs.Raylib;

public class Game
{
    public virtual void InitGame(string[] args)
    {
        Settings.CheckSettings();
        EngineArgParser.ArgInput(args);
        //Lets set up the different nessesary systems!
        EngineRoot.InitEngine();
        SetupGame();
        //This is where all the update loops go.
        MainLoop();
        EngineRoot.ShutdownEngine();
    }

    public virtual void SetupGame()
    {
        //override me.
    }


    void MainLoop()
    {
        while (!WindowShouldClose())
        {
            //Physics, Player logic and so forth. Before draw.
            UpdateLoop();

            //All draw logic.
            DrawUpdate();
            
            //Camera and less priority nesseccary things go here.
            LateUpdate();
        }

        static void UpdateLoop()
        {
            EngineSystems.Update();

        }

        static void DrawUpdate()
        {
            BeginDrawing();
            Backgrounds.DrawBackground();
            EngineSystems.DrawUpdate();
            EndDrawing();
        }

        static void LateUpdate()
        {
            EngineSystems.LateUpdate();
        }
    }
}

