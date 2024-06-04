namespace Engine.Systems.SceneSystem;
using Raylib_cs;
using Engine.Assets;
using Engine.Utils;

public class Logo : Scene
{
    Texture2D raylibLogo;
    Texture2D donutLogo;
    Color textureColor = Color.Blank;
    
    int framesCounter = 0;
    public override void InitScene()
    {
        donutLogo = Textures.GetTexture("DeadDonut-Logo");       
        raylibLogo = Textures.GetTexture("raylib-cs");

    }
    const int splash1 = 120;
    const int splash2 = 240;

    
    public override void DrawScene()
    {
        if (framesCounter < splash1)
        {
            Backgrounds.LerpBackground(Color.RayWhite, 0.05f);
            textureColor = ColorUtil.ColorLerp(textureColor, Color.White, 0.05f);
            Raylib.DrawTexture(raylibLogo, (Raylib.GetScreenWidth() / 2) - (raylibLogo.Width / 2),(Raylib.GetScreenHeight() / 2)  - (raylibLogo.Height / 2), textureColor);
        }
        else if(framesCounter > splash1 && framesCounter < splash2 )
        {
            Backgrounds.LerpBackground(new Color(4, 12, 6, 255), 0.2f);
            Raylib.DrawTexture(donutLogo, (Raylib.GetScreenWidth() / 2) - (donutLogo.Width / 2),(Raylib.GetScreenHeight() / 2)  - (donutLogo.Height / 2), Color.White);
        }
        else if(framesCounter > 121)
        {
            EngineSystems.GetSystem<SceneManager>().SwitchTo(2);
        }
    }
    
    public override void UpdateScene()
    {
        framesCounter++;
    }
    public override void LateUpdateScene()
    {
        
    }
    public override void UnloadScene()
    {
        Textures.UnloadTexture("DeadDonut-Logo");
        Textures.UnloadTexture("raylib-cs");
    }

}
