using Raylib_cs;
using DonutEngine.Backbone;

namespace DonutEngine
{
    namespace Input
    {
        public class InputSystem
        {
            public InputSystem()
            {
                if (Raylib.IsKeyDown(KeyboardKey.KEY_LEFT_ALT) && Raylib.IsKeyPressed(KeyboardKey.KEY_ENTER))
                {
                Raylib.ToggleFullscreen();
                }
                if(Raylib.IsGamepadAvailable(0))
                {

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