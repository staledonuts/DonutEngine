namespace DonutEngine;
using DonutEngine.Backbone.Systems.SceneManager;
using DonutEngine.Backbone.Systems;
using Raylib_cs;

public class DonutLogoSplashScene : Scene
{
    Texture2D raylibLogo;
    Texture2D donutLogo;
    SceneManager? sM = null;

    float exitTime = Sys.settingsContainer.splashScreenLength + Sys.settingsContainer.splashScreenLength;
    
    int framesCounter = 0;



    public void Enter()
    {
        donutLogo = Raylib.LoadTexture(DonutFilePaths.app+"Resources/Splash/DeadDonut-Logo.png");       
        raylibLogo = Raylib.LoadTexture(DonutFilePaths.app+"Resources/Splash/raylib-cs.png");
    }

    public void Exit()
    {
        Raylib.UnloadTexture(donutLogo);
        Raylib.UnloadTexture(raylibLogo);
    }

    public void Init(SceneManager SM)
    {   
        sM = SM;
    }

    public void Update(float deltaTime)
    {
        framesCounter++;
    }
    public void DrawUpdate()
    {
        Raylib.ClearBackground(Color.BLACK);

        if (framesCounter < Sys.settingsContainer.splashScreenLength)
        {
            Raylib.DrawTexture(raylibLogo, (Raylib.GetScreenWidth() / 2) - (raylibLogo.width / 2),(Raylib.GetScreenHeight() / 2)  - (raylibLogo.height / 2),Color.WHITE);
        }
        if(framesCounter > Sys.settingsContainer.splashScreenLength)
        {
            Raylib.DrawTexture(donutLogo, (Raylib.GetScreenWidth() / 2) - (donutLogo.width / 2),(Raylib.GetScreenHeight() / 2)  - (donutLogo.height / 2),Color.WHITE);
        }
        if(framesCounter > exitTime)
        {
            sM?.SwitchTo(2);
        }
    }
}