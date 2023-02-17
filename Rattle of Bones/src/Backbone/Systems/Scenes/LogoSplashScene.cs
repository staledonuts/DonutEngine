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
        splashTex = Raylib.LoadTexture(FilePaths.app+"Assets/Splash/raylib-cs.png");
        DonutSystems.Update += this.Update;
        DonutSystems.DrawUpdate += this.DrawUpdate;
        DonutSystems.LateUpdate += this.LateUpdate;
    }

    public override Scene UnloadScene()
    {
        DonutSystems.Update -= this.Update;
        DonutSystems.DrawUpdate -= this.DrawUpdate;
        DonutSystems.LateUpdate -= this.LateUpdate;
        Raylib.UnloadTexture(splashTex);
        nextScene = new GameplayScene();
        return nextScene;
        
    }

    public override void LateUpdate()
    {
        //throw new NotImplementedException();
    }

    public override void DrawUpdate()
    {
        Raylib.ClearBackground(Color.BLACK);
        Raylib.DrawText(Raylib.GetFPS().ToString(), 12, 24, 20, Color.WHITE);
        Raylib.DrawTexture(splashTex, (Program.settingsVars.screenWidth / 2) - splashTex.width / 2,(Program.settingsVars.screenHeight / 2)  - splashTex.height /2,Color.WHITE);
        Raylib.DrawText("Made With:", Program.settingsVars.screenWidth / 2, (Program.settingsVars.screenHeight / 2) +256, 20, Color.WHITE);
    }   

    public override void Update()
    {
        framesCounter++;    // Count frames

        // Wait for 2 seconds (120 frames) before jumping to TITLE screen
        if (framesCounter > 120)
        {
            // todo: sceneunload
            SceneManager.UnloadScene();
        }
    }


}