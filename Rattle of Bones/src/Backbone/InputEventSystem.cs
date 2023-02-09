using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Raylib_cs;
using static Raylib_cs.Raylib;

namespace DonutEngine.Backbone
{
    public class InputEventSystem
    {
        public delegate void KeyboardEvent(KeyboardKey key);  // delegate
        public delegate void GamepadEvent(GamepadButton button);


        public class ProcessInput
        {
            public static event KeyboardEvent? jumpButton;
            public static event KeyboardEvent? attackButton;
            public static event KeyboardEvent? dashButton;



            public enum ButtonState
            {
                start,
                held,
                canceled,
                released
            }

            public static ButtonState KeyboardInput(KeyboardKey key)
            {
                if(Raylib.IsKeyPressed(key))
                {
                    Console.Write("start");
                    return ButtonState.start;
                }
                else if(Raylib.IsKeyDown(key))
                {
                    Console.Write("held");
                    return ButtonState.held;
                }
                else if(Raylib.IsKeyReleased(key))
                {
                    Console.Write("Released");
                    return ButtonState.released;
                }
                else if(Raylib.IsKeyUp(key))
                {
                    Console.Write("Canceled");
                    return ButtonState.canceled;
                }
                return ButtonState.canceled;
            }


        }
    }
}