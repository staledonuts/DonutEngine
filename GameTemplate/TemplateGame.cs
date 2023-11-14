using Engine;
using Engine.Systems;
using Engine.Systems.SceneSystem;
using Template.Scenes;

namespace Template;

/// <summary>
/// The TemplateGame class inherits from "Game" which has the commandline argument parser and is set up to pre initialize all the base systems.
/// </summary>
public class TemplateGame : Game
{
    /// <param name="gamename"> Here you insert the Window Name </param>
    public TemplateGame(string gamename) : base(gamename){}
    public override void InitGame(string[] args)
    {
        base.InitGame(args);
    }

    /// <summary>
    /// SetupGame() is where you create and add your Custom SystemClass'es to the EngineSystens static class. SystemClass can be found under the Engine.Systems namespace and inherited.
    /// </summary>
    public override void SetupGame()
    {
        EngineSystems.GetSystem<SceneManager>().AddScene(2, new Gameplay());
        base.SetupGame();
    }
}