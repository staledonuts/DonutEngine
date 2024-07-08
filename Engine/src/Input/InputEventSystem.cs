#nullable disable
using System.Numerics;
using Raylib_CSharp.Logging;
using Raylib_CSharp.Interact;
namespace Engine.Systems.EngineInput;


public static class InputEventSystem
{
    public delegate void InputEventHandler(bool input);
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
        Logger.TraceLog(TraceLogLevel.Info, "Initialized Input System");
    }
    // Define a method that raises the event.
    public static void UpdateInputEvent()
    {
        // Keyboard
        CrossButtonEvent?.Invoke(Input.IsKeyPressed(KeyboardKey.Z));
        RectangleButtonEvent?.Invoke(Input.IsKeyDown(KeyboardKey.X));
        R1ButtonEvent?.Invoke(Input.IsKeyDown(KeyboardKey.C));
        DpadEvent?.Invoke(Vector2Composite(useKeyboard:true));
        ToggleUI?.Invoke(Input.IsKeyPressed(KeyboardKey.F11));
            
        // Gamepad
        if(Input.IsGamepadAvailable(0))
        {
            CrossButtonEvent?.Invoke(Input.IsGamepadButtonPressed(0, GamepadButton.RightFaceDown));
            RectangleButtonEvent?.Invoke(Input.IsGamepadButtonPressed(0, GamepadButton.RightFaceLeft));
            CircleButtonEvent?.Invoke(Input.IsGamepadButtonPressed(0, GamepadButton.RightFaceRight));
            TriangleButtonEvent?.Invoke(Input.IsGamepadButtonPressed(0, GamepadButton.RightFaceUp));
            R1ButtonEvent?.Invoke(Input.IsGamepadButtonPressed(0, GamepadButton.RightTrigger1));
            L1ButtonEvent?.Invoke(Input.IsGamepadButtonPressed(0, GamepadButton.LeftTrigger1));
            DpadEvent?.Invoke(Vector2Composite(useKeyboard:false));
            LeftStickEvent?.Invoke(LeftStick(0));
            RightStickEvent?.Invoke(RightStick(0));
            SelectButtonEvent?.Invoke(Input.IsGamepadButtonPressed(0, GamepadButton.MiddleLeft));
            StartButtonEvent?.Invoke(Input.IsGamepadButtonPressed(0, GamepadButton.MiddleRight));
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
                gamePadInput0.X = Input.GetGamepadAxisMovement(0, GamepadAxis.LeftX);
                gamePadInput0.Y = Input.GetGamepadAxisMovement(0, GamepadAxis.LeftY);
                return gamePadInput0;
            }
            case 1:
            {
                gamePadInput1.X = Input.GetGamepadAxisMovement(1, GamepadAxis.LeftX);
                gamePadInput1.Y = Input.GetGamepadAxisMovement(1, GamepadAxis.LeftY);
                return gamePadInput1;
            }
            case 2:
            {
                gamePadInput2.X = Input.GetGamepadAxisMovement(2, GamepadAxis.LeftX);
                gamePadInput2.Y = Input.GetGamepadAxisMovement(2, GamepadAxis.LeftY);
                return gamePadInput2;
            }
            case 3:
            {
                gamePadInput3.X = Input.GetGamepadAxisMovement(3, GamepadAxis.LeftX);
                gamePadInput3.Y = Input.GetGamepadAxisMovement(3, GamepadAxis.LeftY);
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
                gamePadInput0.X = Input.GetGamepadAxisMovement(0, GamepadAxis.RightX);
                gamePadInput0.Y = Input.GetGamepadAxisMovement(0, GamepadAxis.RightY);
                return gamePadInput0;
            }
            case 1:
            {
                gamePadInput1.X = Input.GetGamepadAxisMovement(1, GamepadAxis.RightX);
                gamePadInput1.Y = Input.GetGamepadAxisMovement(1, GamepadAxis.RightY);
                return gamePadInput1;
            }
            case 2:
            {
                gamePadInput2.X = Input.GetGamepadAxisMovement(2, GamepadAxis.RightX);
                gamePadInput2.Y = Input.GetGamepadAxisMovement(2, GamepadAxis.RightY);
                return gamePadInput2;
            }
            case 3:
            {
                gamePadInput3.X = Input.GetGamepadAxisMovement(3, GamepadAxis.RightX);
                gamePadInput3.Y = Input.GetGamepadAxisMovement(3, GamepadAxis.RightY);
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
            gamePadInput0.X = -Convert.ToInt32(Input.IsKeyDown(KeyboardKey.Left)) +Convert.ToInt32(Input.IsKeyDown(KeyboardKey.Right));
            gamePadInput0.Y = -Convert.ToInt32(Input.IsKeyDown(KeyboardKey.Up)) +Convert.ToInt32(Input.IsKeyDown(KeyboardKey.Down));
            return gamePadInput0;
        }
        else
        {
            gamePadInput0.X = -Convert.ToInt32(Input.IsGamepadButtonDown(0, GamepadButton.LeftFaceLeft)) +Convert.ToInt32(Input.IsGamepadButtonDown(0, GamepadButton.LeftFaceRight));
            gamePadInput0.Y = -Convert.ToInt32(Input.IsGamepadButtonDown(0, GamepadButton.LeftFaceUp)) +Convert.ToInt32(Input.IsGamepadButtonDown(0, GamepadButton.LeftFaceDown));
            if(Input.IsGamepadAvailable(1))
            {
                gamePadInput1.X = -Convert.ToInt32(Input.IsGamepadButtonDown(1, GamepadButton.LeftFaceLeft)) +Convert.ToInt32(Input.IsGamepadButtonDown(0, GamepadButton.LeftFaceRight));
                gamePadInput1.Y = -Convert.ToInt32(Input.IsGamepadButtonDown(1, GamepadButton.LeftFaceUp)) +Convert.ToInt32(Input.IsGamepadButtonDown(0, GamepadButton.LeftFaceDown));
                return gamePadInput1;
            }
            if(Input.IsGamepadAvailable(2))
            {
                gamePadInput2.X = -Convert.ToInt32(Input.IsGamepadButtonDown(2, GamepadButton.LeftFaceLeft)) +Convert.ToInt32(Input.IsGamepadButtonDown(0, GamepadButton.LeftFaceRight));
                gamePadInput2.Y = -Convert.ToInt32(Input.IsGamepadButtonDown(2, GamepadButton.LeftFaceUp)) +Convert.ToInt32(Input.IsGamepadButtonDown(0, GamepadButton.LeftFaceDown));
                return gamePadInput2;
            }
            if(Input.IsGamepadAvailable(3))
            {
                gamePadInput3.X = -Convert.ToInt32(Input.IsGamepadButtonDown(3, GamepadButton.LeftFaceLeft)) +Convert.ToInt32(Input.IsGamepadButtonDown(0, GamepadButton.LeftFaceRight));
                gamePadInput3.Y = -Convert.ToInt32(Input.IsGamepadButtonDown(3, GamepadButton.LeftFaceUp)) +Convert.ToInt32(Input.IsGamepadButtonDown(0, GamepadButton.LeftFaceDown));
                return gamePadInput3;
            }
            return gamePadInput0;
        }
    }

}