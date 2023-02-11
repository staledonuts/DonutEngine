using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace DonutEngine.Backbone
{
    public struct InputVars
    {
        public static bool jumpButton;
        public static bool attackButton;
        public static bool dashButton;

        public static Vector2 dpad = new();
    }
    public class InputEventSystem
    {
        public delegate void KeyboardEvent();  // delegate
        public delegate void GamepadEvent();


        public class ProcessInput
        {
            public static event KeyboardEvent? jumpEvent;
            public static event KeyboardEvent? attackEvent;
            public static event KeyboardEvent? dashEvent;

            public static void UpdateInput()
            {
                InputVars.jumpButton = Raylib.IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_DOWN) || Raylib.IsKeyDown(KeyboardKey.KEY_X);
                InputVars.attackButton = Raylib.IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_LEFT) || Raylib.IsKeyDown(KeyboardKey.KEY_C);
                InputVars.dashButton = Raylib.IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_DOWN) || Raylib.IsKeyDown(KeyboardKey.KEY_Z);
                InputVars.dpad.X = -(int)Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) +(int)Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT);
                InputVars.dpad.Y = -(int)Raylib.IsKeyDown(KeyboardKey.KEY_UP) +(int)Raylib.IsKeyDown(KeyboardKey.KEY_DOWN);
            }   

        }
    }
}