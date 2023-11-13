using Template;

static class Program
{
    static Game game = new TemplateGame("Template - DonutEngine");
    public static void Main(string[] args)
    {
        game.InitGame(args);
    }
}

 