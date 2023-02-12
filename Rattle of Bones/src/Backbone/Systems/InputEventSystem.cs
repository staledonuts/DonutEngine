using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;

namespace DonutEngine.Backbone.Systems
{
    public static class InputVars
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
            DonutSystems.Update += UpdateInput;
        }

        public static void UpdateInput()
        {
            InputVars.jumpButton = Raylib.IsKeyDown(KeyboardKey.KEY_X);
            InputVars.attackButton = Raylib.IsKeyDown(KeyboardKey.KEY_C);
            InputVars.dashButton = Raylib.IsKeyDown(KeyboardKey.KEY_Z);
            InputVars.dpad.X = -(int)Raylib.IsKeyDown(KeyboardKey.KEY_LEFT) +(int)Raylib.IsKeyDown(KeyboardKey.KEY_RIGHT);
            InputVars.dpad.Y = -(int)Raylib.IsKeyDown(KeyboardKey.KEY_UP) +(int)Raylib.IsKeyDown(KeyboardKey.KEY_DOWN);
        }   

    }

}