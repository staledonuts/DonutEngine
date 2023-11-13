using Engine;
using Engine.Systems;
using Engine.Systems.Input;
using Template;
using static Raylib_cs.Raylib;

static class Program
{
    static Game game = new TemplateGame("Template - DonutEngine");
    public static void Main(string[] args)
    {
        game.InitGame(args);
    }
}

 