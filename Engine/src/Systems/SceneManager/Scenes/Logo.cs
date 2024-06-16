namespace Engine.Systems.SceneSystem;
using Raylib_cs;
using Engine.Assets;
using Engine.Utils;
using System.Numerics;

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
    Vector2 position = new();
    
    public override void DrawScene()
    {
        if (framesCounter < splash1)
        {
            position.X = (Raylib.GetScreenWidth() / 2) - (raylibLogo.Width / 2);
            position.Y = (Raylib.GetScreenHeight() / 2)  - (raylibLogo.Height / 2);
            Backgrounds.LerpBackground(Color.RayWhite, 0.05f);
            textureColor = ColorUtil.ColorLerp(textureColor, Color.White, 0.05f);
            Draw2D.DrawImage(1, raylibLogo, position, textureColor);
        }
        else if(framesCounter > splash1 && framesCounter < splash2 )
        {
            position.X = (Raylib.GetScreenWidth() / 2) - (donutLogo.Width / 2);
            position.Y = (Raylib.GetScreenHeight() / 2)  - (donutLogo.Height / 2);
            Backgrounds.LerpBackground(Color.DarkBlue, 0.2f);
            Draw2D.DrawImage(1, donutLogo, position, Color.White);
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
