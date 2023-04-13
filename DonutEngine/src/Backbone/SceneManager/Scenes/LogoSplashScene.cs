namespace DonutEngine;
using DonutEngine.Backbone.Systems;
using Raylib_cs;

public class LogoSplashScene : Scene
{

    static Texture2D splashTex;
    private Scene? nextScene;
    int framesCounter = 0;

    public override void InitializeScene()
    {
        splashTex = Raylib.LoadTexture(DonutFilePaths.app+"Resources/Splash/raylib-cs.png");
        DonutSystems.Update += this.Update;
        DonutSystems.DrawUpdate += this.DrawUpdate;
    }

    public override Scene UnloadScene()
    {
        DonutSystems.Update -= this.Update;
        DonutSystems.DrawUpdate -= this.DrawUpdate;
        Raylib.UnloadTexture(splashTex);
        return nextScene = new DonutLogoSplashScene();
        
    }

    public override void LateUpdate()
    {
        //throw new NotImplementedException();
    }

    public override void DrawUpdate()
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawTexture(splashTex, (DonutSystems.settingsVars.screenWidth / 2) - splashTex.width / 2,(DonutSystems.settingsVars.screenHeight / 2)  - splashTex.height /2,Color.WHITE);
    }   

    public override void Update()
    {
        framesCounter++;    // Count frames

        // Wait for 2 seconds (120 frames) before jumping to TITLE screen
        if (framesCounter > DonutSystems.settingsVars.splashScreenLength)
        {
            // todo: sceneunload
            SceneManager.UnloadScene();
        }
    }


}