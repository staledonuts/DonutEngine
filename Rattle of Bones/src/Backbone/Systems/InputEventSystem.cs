using Raylib_cs;
using static Raylib_cs.Raylib;
using System.Numerics;
using System;
namespace DonutEngine.Backbone.Systems;


public static class InputEventSystem
{
    public delegate void InputEventHandler(KeyboardKey key);

    // Declare the event using the delegate.
    public static event InputEventHandler InputEvent;

    // Define a method that raises the event.
    public static void UpdateInputEvent(KeyboardKey key, InputState state)
    {
        // Check if there are any subscribers to the event.
        if (InputEvent != null)
        {
            InputEvent.Invoke(key, state);   
        }
    }
    public static void Update()
    {
        
    }

    

}

