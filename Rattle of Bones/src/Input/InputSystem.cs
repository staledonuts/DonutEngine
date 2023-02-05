using Raylib_cs;

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
        }

    }
}