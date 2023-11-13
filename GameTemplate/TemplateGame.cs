using Engine;
using Engine.Systems;
using Engine.Systems.SceneSystem;
using Template.Scenes;

namespace Template;

public class TemplateGame : Game
{
    public TemplateGame(string gamename) : base(gamename){}
    public override void InitGame(string[] args)
    {
        base.InitGame(args);
    }

    public override void SetupGame()
    {
        EngineSystems.GetSystem<SceneManager>().AddScene(2, new Gameplay());
        base.SetupGame();
    }
}