using Raylib_cs;
using static Raylib_cs.Raylib;
using Box2DX.Common;
namespace DonutEngine.Backbone.Systems;


public static class InputEventSystem
{
    public delegate void InputEventHandler(CBool input);
    public delegate void InputEventDpadHandler(Vec2 vector2);

    // Declare the event using the delegate.
    public static event InputEventHandler? DashEvent;
    public static event InputEventHandler? JumpEvent;
    public static event InputEventHandler? AttackEvent;
    public static event InputEventDpadHandler? DpadEvent;

    // Define a method that raises the event.
    public static void UpdateInputEvent()
    {
        if(!IsGamepadAvailable(0))
        {
            JumpEvent?.Invoke(IsKeyDown(KeyboardKey.KEY_Z));
            AttackEvent?.Invoke(IsKeyDown(KeyboardKey.KEY_X));
            DashEvent?.Invoke(IsKeyDown(KeyboardKey.KEY_C));
            DpadEvent?.Invoke(Vector2Composite());
            
        }
        else
        {
            JumpEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_DOWN));
            AttackEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_LEFT));
            DashEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.GAMEPAD_BUTTON_RIGHT_TRIGGER_1));
            DpadEvent?.Invoke(Vector2Composite());
        }
    }

    static Vec2 Vector2Composite()
    {
        Vec2 input;
        if(!IsGamepadAvailable(0))
        {
            input.X = -(int)IsKeyDown(KeyboardKey.KEY_LEFT) +(int)IsKeyDown(KeyboardKey.KEY_RIGHT);
            input.Y = -(int)IsKeyDown(KeyboardKey.KEY_UP) +(int)IsKeyDown(KeyboardKey.KEY_DOWN);
            return input;
        }
        else
        {
            input.X = -(int)IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_LEFT) +(int)IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_RIGHT);
            input.Y = -(int)IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_UP) +(int)IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_DOWN);
            return input;
        }
    }

}

