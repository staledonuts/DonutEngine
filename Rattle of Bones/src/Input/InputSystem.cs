using Raylib_cs;
using System.Numerics;
using DonutEngine.Backbone;

namespace DonutEngine
{
    namespace Input
    {
        public class InputSystem
        {
            public enum ButtonState
            {
                start,
                held,
                canceled,
                released
            }

            public static Vector2 vectorComposite = new(0,0);
            public void KeyboardInput()
            {
                
            }

            public static void UpdateInputVectorComposite()
            {
                    if(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) || Raylib.IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_LEFT))
                    {
                        vectorComposite.X = 1;
                    }
                    if(Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT) || Raylib.IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_RIGHT))
                    {
                        vectorComposite.X = -1;
                    }
                    if(Raylib.IsKeyDown(KeyboardKey.KEY_UP) || Raylib.IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_UP))
                    {
                        vectorComposite.Y = 1;
                    }
                    if(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN) || Raylib.IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_DOWN))
                    {
                        vectorComposite.Y = -1;
                    }
                    else
                    {
                        vectorComposite = Vector2.Zero;
                    }
            }

        public void InputHandler()
        {
            if(Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_ALT) && Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
            {
            Raylib.ToggleFullscreen();
            }
            if(Raylib.IsGamepadAvailable(0))
            {

            }
            if(Raylib.IsKeyDown(KeyboardKey.KEY_DOWN))
            {
                
            }
        }
        }

    }
}