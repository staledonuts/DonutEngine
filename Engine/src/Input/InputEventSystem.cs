#nullable disable
using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
namespace Engine.Systems.Input;


public static class InputEventSystem
{
    public delegate void InputEventHandler(CBool input);
    public delegate void InputEventDpadHandler(Vector2 vector2);

    // Declare the event using the delegate.
    public static event InputEventHandler L1ButtonEvent;
    public static event InputEventHandler R1ButtonEvent;
    public static event InputEventHandler CrossButtonEvent;
    public static event InputEventHandler RectangleButtonEvent;
    public static event InputEventHandler CircleButtonEvent;
    public static event InputEventHandler TriangleButtonEvent;
    public static event InputEventHandler StartButtonEvent;
    public static event InputEventHandler SelectButtonEvent;
    public static event InputEventDpadHandler DpadEvent;
    public static event InputEventDpadHandler LeftStickEvent;
    public static event InputEventDpadHandler RightStickEvent;
    public static event InputEventHandler ToggleUI;

    public static void Initialize()
    {
        Raylib.TraceLog(TraceLogLevel.LOG_INFO, "Initialized Input System");
    }
    // Define a method that raises the event.
    public static void UpdateInputEvent()
    {
        // Keyboard
        CrossButtonEvent?.Invoke(IsKeyPressed(KeyboardKey.KEY_Z));
        RectangleButtonEvent?.Invoke(IsKeyDown(KeyboardKey.KEY_X));
        R1ButtonEvent?.Invoke(IsKeyDown(KeyboardKey.KEY_C));
        DpadEvent?.Invoke(Vector2Composite(useKeyboard:true));
        ToggleUI?.Invoke(IsKeyPressed(KeyboardKey.KEY_F11));
            
        // Gamepad
        if(Raylib.IsGamepadAvailable(0))
        {
            CrossButtonEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_DOWN));
            RectangleButtonEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_LEFT));
            CircleButtonEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_RIGHT));
            TriangleButtonEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.GAMEPAD_BUTTON_RIGHT_FACE_UP));
            R1ButtonEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.GAMEPAD_BUTTON_RIGHT_TRIGGER_1));
            L1ButtonEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.GAMEPAD_BUTTON_LEFT_TRIGGER_1));
            DpadEvent?.Invoke(Vector2Composite(useKeyboard:false));
            LeftStickEvent?.Invoke(LeftStick(0));
            RightStickEvent?.Invoke(RightStick(0));
            SelectButtonEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.GAMEPAD_BUTTON_MIDDLE_LEFT));
            StartButtonEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.GAMEPAD_BUTTON_MIDDLE_RIGHT));

        }
    }

    static Vector2 LeftStick(int player)
    {
        Vector2 gamePadInput0;
        Vector2 gamePadInput1;
        Vector2 gamePadInput2;
        Vector2 gamePadInput3;
        switch (player)
        {
            case 0:
            {
                gamePadInput0.X = GetGamepadAxisMovement(0, GamepadAxis.GAMEPAD_AXIS_LEFT_X);
                gamePadInput0.Y = GetGamepadAxisMovement(0, GamepadAxis.GAMEPAD_AXIS_LEFT_Y);
                return gamePadInput0;
            }
            case 1:
            {
                gamePadInput1.X = GetGamepadAxisMovement(1, GamepadAxis.GAMEPAD_AXIS_LEFT_X);
                gamePadInput1.Y = GetGamepadAxisMovement(1, GamepadAxis.GAMEPAD_AXIS_LEFT_Y);
                return gamePadInput1;
            }
            case 2:
            {
                gamePadInput2.X = GetGamepadAxisMovement(2, GamepadAxis.GAMEPAD_AXIS_LEFT_X);
                gamePadInput2.Y = GetGamepadAxisMovement(2, GamepadAxis.GAMEPAD_AXIS_LEFT_Y);
                return gamePadInput2;
            }
            case 3:
            {
                gamePadInput3.X = GetGamepadAxisMovement(3, GamepadAxis.GAMEPAD_AXIS_LEFT_X);
                gamePadInput3.Y = GetGamepadAxisMovement(3, GamepadAxis.GAMEPAD_AXIS_LEFT_Y);
                return gamePadInput3;
            }
            default:
            {
                return Vector2.Zero;
            }
        }
    }

    static Vector2 RightStick(int player)
    {
        Vector2 gamePadInput0;
        Vector2 gamePadInput1;
        Vector2 gamePadInput2;
        Vector2 gamePadInput3;
        switch (player)
        {
            case 0:
            {
                gamePadInput0.X = GetGamepadAxisMovement(0, GamepadAxis.GAMEPAD_AXIS_RIGHT_X);
                gamePadInput0.Y = GetGamepadAxisMovement(0, GamepadAxis.GAMEPAD_AXIS_RIGHT_Y);
                return gamePadInput0;
            }
            case 1:
            {
                gamePadInput1.X = GetGamepadAxisMovement(1, GamepadAxis.GAMEPAD_AXIS_RIGHT_X);
                gamePadInput1.Y = GetGamepadAxisMovement(1, GamepadAxis.GAMEPAD_AXIS_RIGHT_Y);
                return gamePadInput1;
            }
            case 2:
            {
                gamePadInput2.X = GetGamepadAxisMovement(2, GamepadAxis.GAMEPAD_AXIS_RIGHT_X);
                gamePadInput2.Y = GetGamepadAxisMovement(2, GamepadAxis.GAMEPAD_AXIS_RIGHT_Y);
                return gamePadInput2;
            }
            case 3:
            {
                gamePadInput3.X = GetGamepadAxisMovement(3, GamepadAxis.GAMEPAD_AXIS_RIGHT_X);
                gamePadInput3.Y = GetGamepadAxisMovement(3, GamepadAxis.GAMEPAD_AXIS_RIGHT_Y);
                return gamePadInput3;
            }
            default:
            {
                return Vector2.Zero;
            }
        }
    }

    static Vector2 Vector2Composite(bool useKeyboard = true)
    {
        Vector2 gamePadInput0;
        Vector2 gamePadInput1;
        Vector2 gamePadInput2;
        Vector2 gamePadInput3;

        if(useKeyboard)
        {
            gamePadInput0.X = -(int)IsKeyDown(KeyboardKey.KEY_LEFT) +(int)IsKeyDown(KeyboardKey.KEY_RIGHT);
            gamePadInput0.Y = -(int)IsKeyDown(KeyboardKey.KEY_UP) +(int)IsKeyDown(KeyboardKey.KEY_DOWN);
            return gamePadInput0;
        }
        else
        {
            gamePadInput0.X = -(int)IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_LEFT) +(int)IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_RIGHT);
            gamePadInput0.Y = -(int)IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_UP) +(int)IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_DOWN);
            if(Raylib.IsGamepadAvailable(1))
            {
                gamePadInput1.X = -(int)IsGamepadButtonDown(1, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_LEFT) +(int)IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_RIGHT);
                gamePadInput1.Y = -(int)IsGamepadButtonDown(1, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_UP) +(int)IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_DOWN);
                return gamePadInput1;
            }
            if(Raylib.IsGamepadAvailable(2))
            {
                gamePadInput2.X = -(int)IsGamepadButtonDown(2, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_LEFT) +(int)IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_RIGHT);
                gamePadInput2.Y = -(int)IsGamepadButtonDown(2, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_UP) +(int)IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_DOWN);
                return gamePadInput2;
            }
            if(Raylib.IsGamepadAvailable(3))
            {
                gamePadInput3.X = -(int)IsGamepadButtonDown(3, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_LEFT) +(int)IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_RIGHT);
                gamePadInput3.Y = -(int)IsGamepadButtonDown(3, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_UP) +(int)IsGamepadButtonDown(0, GamepadButton.GAMEPAD_BUTTON_LEFT_FACE_DOWN);
                return gamePadInput3;
            }
            return gamePadInput0;
        }
    }

}