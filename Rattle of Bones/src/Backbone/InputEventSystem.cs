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
        public static bool skullButton;
        public static Vector2 dpad = new();
    }
    public class InputEventSystem
    {
        public delegate void InputEvent();
        public static event InputEvent? jumpEvent;
        public static event InputEvent? attackEvent;
        public static event InputEvent? dashEvent;
        public static event InputEvent? skullEvent;
        public static event InputEvent? horizontalInputEvent;
        public static event InputEvent? verticalInputEvent;

        public static void InitInputSystem()
        {
            jumpEvent.Invoke();
            attackEvent.Invoke();
            dashEvent.Invoke();
            skullEvent.Invoke();
            horizontalInputEvent.Invoke();
            verticalInputEvent.Invoke();
        }

        
        public static void UpdateInput()
        {
            jumpEvent.Invoke();
            attackEvent.Invoke();
            dashEvent.Invoke();
            skullEvent.Invoke();
            horizontalInputEvent.Invoke();
            verticalInputEvent.Invoke();
            InputVars.jumpButton = Raylib.IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_DOWN) || Raylib.IsKeyDown(KeyboardKey.KEY_X);
            InputVars.attackButton = Raylib.IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_LEFT) || Raylib.IsKeyDown(KeyboardKey.KEY_C);
            InputVars.dashButton = Raylib.IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_DOWN) || Raylib.IsKeyDown(KeyboardKey.KEY_Z);
            InputVars.dpad.X = -(int)Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) +(int)Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT);
            InputVars.dpad.Y = -(int)Raylib.IsKeyDown(KeyboardKey.KEY_UP) +(int)Raylib.IsKeyDown(KeyboardKey.KEY_DOWN);
        }   

    }

}