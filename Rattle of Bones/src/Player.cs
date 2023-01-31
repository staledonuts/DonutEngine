using static Raylib_cs.Raylib;
using DonutEngine.Input;
using Raylib_cs;

namespace DonutEngine
{
    public class Player : PlayerInput
    {   
        
        Texture2D playerSprite = LoadTexture("/Assets/Sprites/Player/Player.png");
        
        public override void update()
        {
            base.update();
            DrawCircle((int)transform.translation.X,(int)transform.translation.Y,20,Color.BLUE);
            Tasks();
        }

        public void Tasks()
        {
            if(Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
            {
                transform.translation.Y += 20;
            }
        }


    }
}