using static Raylib_cs.Raylib;
using Raylib_cs;

namespace DonutEngine
{
    public class Player : PlayerBehaviour
    {
        public override void Update(float deltaTime)
        {
            base.Update(deltaTime);
            Tasks();
        }

        public void Tasks()
        {
            Raylib.DrawCircle((int)transform.position.X,(int)transform.position.Y,10f,Color.BLACK);
            if(Raylib.IsGamepadButtonDown(0,GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_DOWN))
            {
                transform.position.Y -= 3;
            }


        }


    }
}