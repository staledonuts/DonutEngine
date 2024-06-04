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
        Raylib.TraceLog(TraceLogLevel.Info, "Initialized Input System");
    }
    // Define a method that raises the event.
    public static void UpdateInputEvent()
    {
        // Keyboard
        CrossButtonEvent?.Invoke(IsKeyPressed(KeyboardKey.Z));
        RectangleButtonEvent?.Invoke(IsKeyDown(KeyboardKey.X));
        R1ButtonEvent?.Invoke(IsKeyDown(KeyboardKey.C));
        DpadEvent?.Invoke(Vector2Composite(useKeyboard:true));
        ToggleUI?.Invoke(IsKeyPressed(KeyboardKey.F11));
            
        // Gamepad
        if(Raylib.IsGamepadAvailable(0))
        {
            CrossButtonEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.RightFaceDown));
            RectangleButtonEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.RightFaceLeft));
            CircleButtonEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.RightFaceRight));
            TriangleButtonEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.RightFaceUp));
            R1ButtonEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.RightTrigger1));
            L1ButtonEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.LeftTrigger1));
            DpadEvent?.Invoke(Vector2Composite(useKeyboard:false));
            LeftStickEvent?.Invoke(LeftStick(0));
            RightStickEvent?.Invoke(RightStick(0));
            SelectButtonEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.MiddleLeft));
            StartButtonEvent?.Invoke(IsGamepadButtonPressed(0, GamepadButton.MiddleRight));

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
                gamePadInput0.X = GetGamepadAxisMovement(0, GamepadAxis.LeftX);
                gamePadInput0.Y = GetGamepadAxisMovement(0, GamepadAxis.LeftY);
                return gamePadInput0;
            }
            case 1:
            {
                gamePadInput1.X = GetGamepadAxisMovement(1, GamepadAxis.LeftX);
                gamePadInput1.Y = GetGamepadAxisMovement(1, GamepadAxis.LeftY);
                return gamePadInput1;
            }
            case 2:
            {
                gamePadInput2.X = GetGamepadAxisMovement(2, GamepadAxis.LeftX);
                gamePadInput2.Y = GetGamepadAxisMovement(2, GamepadAxis.LeftY);
                return gamePadInput2;
            }
            case 3:
            {
                gamePadInput3.X = GetGamepadAxisMovement(3, GamepadAxis.LeftX);
                gamePadInput3.Y = GetGamepadAxisMovement(3, GamepadAxis.LeftY);
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
                gamePadInput0.X = GetGamepadAxisMovement(0, GamepadAxis.RightX);
                gamePadInput0.Y = GetGamepadAxisMovement(0, GamepadAxis.RightY);
                return gamePadInput0;
            }
            case 1:
            {
                gamePadInput1.X = GetGamepadAxisMovement(1, GamepadAxis.RightX);
                gamePadInput1.Y = GetGamepadAxisMovement(1, GamepadAxis.RightY);
                return gamePadInput1;
            }
            case 2:
            {
                gamePadInput2.X = GetGamepadAxisMovement(2, GamepadAxis.RightX);
                gamePadInput2.Y = GetGamepadAxisMovement(2, GamepadAxis.RightY);
                return gamePadInput2;
            }
            case 3:
            {
                gamePadInput3.X = GetGamepadAxisMovement(3, GamepadAxis.RightX);
                gamePadInput3.Y = GetGamepadAxisMovement(3, GamepadAxis.RightY);
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
            gamePadInput0.X = -(int)IsKeyDown(KeyboardKey.Left) +(int)IsKeyDown(KeyboardKey.Right);
            gamePadInput0.Y = -(int)IsKeyDown(KeyboardKey.Up) +(int)IsKeyDown(KeyboardKey.Down);
            return gamePadInput0;
        }
        else
        {
            gamePadInput0.X = -(int)IsGamepadButtonDown(0, GamepadButton.LeftFaceLeft) +(int)IsGamepadButtonDown(0, GamepadButton.LeftFaceRight);
            gamePadInput0.Y = -(int)IsGamepadButtonDown(0, GamepadButton.LeftFaceUp) +(int)IsGamepadButtonDown(0, GamepadButton.LeftFaceDown);
            if(Raylib.IsGamepadAvailable(1))
            {
                gamePadInput1.X = -(int)IsGamepadButtonDown(1, GamepadButton.LeftFaceLeft) +(int)IsGamepadButtonDown(0, GamepadButton.LeftFaceRight);
                gamePadInput1.Y = -(int)IsGamepadButtonDown(1, GamepadButton.LeftFaceUp) +(int)IsGamepadButtonDown(0, GamepadButton.LeftFaceDown);
                return gamePadInput1;
            }
            if(Raylib.IsGamepadAvailable(2))
            {
                gamePadInput2.X = -(int)IsGamepadButtonDown(2, GamepadButton.LeftFaceLeft) +(int)IsGamepadButtonDown(0, GamepadButton.LeftFaceRight);
                gamePadInput2.Y = -(int)IsGamepadButtonDown(2, GamepadButton.LeftFaceUp) +(int)IsGamepadButtonDown(0, GamepadButton.LeftFaceDown);
                return gamePadInput2;
            }
            if(Raylib.IsGamepadAvailable(3))
            {
                gamePadInput3.X = -(int)IsGamepadButtonDown(3, GamepadButton.LeftFaceLeft) +(int)IsGamepadButtonDown(0, GamepadButton.LeftFaceRight);
                gamePadInput3.Y = -(int)IsGamepadButtonDown(3, GamepadButton.LeftFaceUp) +(int)IsGamepadButtonDown(0, GamepadButton.LeftFaceDown);
                return gamePadInput3;
            }
            return gamePadInput0;
        }
    }

}