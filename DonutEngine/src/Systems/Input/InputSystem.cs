using Raylib_cs;
using System.Numerics;
using System.Runtime.InteropServices;
using DonutEngine.Utilities;
using Newtonsoft.Json;


namespace DonutEngine.Input
{
    #region JSON Data Transfer Objects (DTOs)

    internal class ActionMapCollectionDTO
    {
        public List<ActionMapDTO> ActionMaps { get; set; } = new List<ActionMapDTO>();
    }
    
    internal class ActionMapDTO
    {
        public string Name { get; set; }
        public List<ActionDTO> Actions { get; set; } = new List<ActionDTO>();
    }

    internal class ActionDTO
    {
        public string Name { get; set; }
        public string ControlScheme { get; set; } // e.g. "Gamepad", "Keyboard"
        public List<string> Bindings { get; set; } = new List<string>();
    }

    #endregion

    #region Core Input System Abstractions
    
    public readonly struct CallbackContext
    {
        public readonly InputAction Action;
        public readonly object SourceControl;
        public CallbackContext(InputAction action, object sourceControl) { Action = action; SourceControl = sourceControl; }
        public TValue ReadValue<TValue>() where TValue : struct => SourceControl is InputControl<TValue> control ? control.Value : default;
    }

    public class InputAction
    {
        public string Name { get; }
        public IReadOnlyList<InputBinding> Bindings => _bindings;
        private readonly List<InputBinding> _bindings = new List<InputBinding>();
        public event Action<CallbackContext> Started;
        public event Action<CallbackContext> Performed;
        public event Action<CallbackContext> Canceled;
        public InputAction(string name) { Name = name; }
        internal void AddBinding(InputBinding binding) => _bindings.Add(binding);
        internal void TriggerStarted(CallbackContext context) => Started?.Invoke(context);
        internal void TriggerPerformed(CallbackContext context) => Performed?.Invoke(context);
        internal void TriggerCanceled(CallbackContext context) => Canceled?.Invoke(context);
    }

    public class InputBinding
    {
        public string Path { get; }
        public string Action { get; }
        public string ControlScheme { get; }
        public InputBinding(string path, string action, string controlScheme) { Path = path; Action = action; ControlScheme = controlScheme; }
    }

    public class InputActionMap
    {
        public string Name { get; }
        public IReadOnlyDictionary<string, InputAction> Actions => _actions;
        private readonly Dictionary<string, InputAction> _actions = new Dictionary<string, InputAction>();
        public bool IsEnabled { get; private set; }
        public InputActionMap(string name) { Name = name; }
        public void AddAction(InputAction action, IEnumerable<InputBinding> bindings) { _actions[action.Name] = action; foreach (var binding in bindings) action.AddBinding(binding); }
        public InputAction FindAction(string name) { _actions.TryGetValue(name, out var action); return action; }
        public void Enable() => IsEnabled = true;
        public void Disable() => IsEnabled = false;
    }

    #endregion

    #region Devices and Controls
    
    public class InputControl<TValue> where TValue : struct
    {
        public TValue Value { get; private set; }
        public TValue PreviousValue { get; private set; }
        private const float ButtonPressThreshold = 0.5f;
        public bool IsPressed => ReadValueAsFloat() > ButtonPressThreshold;
        public bool WasPressed => ReadPreviousValueAsFloat() > ButtonPressThreshold;
        public void UpdateState(TValue newValue) { PreviousValue = Value; Value = newValue; }
        private float ReadValueAsFloat() { if (Value is bool b) return b ? 1.0f : 0.0f; if (Value is float f) return f; if (Value is Vector2 v) return v.Length(); return 0.0f; }
        private float ReadPreviousValueAsFloat() { if (PreviousValue is bool b) return b ? 1.0f : 0.0f; if (PreviousValue is float f) return f; if (PreviousValue is Vector2 v) return v.Length(); return 0.0f; }
    }
    
    public abstract class InputDevice
    {
        public int DeviceId { get; }
        public string Name { get; protected set; }
        public bool IsConnected { get; set; }
        protected InputDevice(int deviceId, string name) { DeviceId = deviceId; Name = name; IsConnected = true; }
        public abstract void Update();
        public abstract InputControl<TValue> GetControl<TValue>(string controlName) where TValue : struct;
    }

    public class KeyboardDevice : InputDevice
    {
        public readonly Dictionary<KeyboardKey, InputControl<bool>> Keys = new Dictionary<KeyboardKey, InputControl<bool>>();
        public KeyboardDevice() : base(0, "Keyboard") { foreach (KeyboardKey key in Enum.GetValues(typeof(KeyboardKey))) if (key != KeyboardKey.Null) Keys[key] = new InputControl<bool>(); }
        public override void Update() { foreach (var pair in Keys) pair.Value.UpdateState(Raylib.IsKeyDown(pair.Key)); }
        public override InputControl<TValue> GetControl<TValue>(string controlName) where TValue : struct { if (Enum.TryParse<KeyboardKey>(controlName, true, out var key) && Keys.TryGetValue(key, out var control)) return control as InputControl<TValue>; return null; }
    }

    public class GamepadDevice : InputDevice
    {
        public readonly Dictionary<GamepadButton, InputControl<bool>> Buttons = new Dictionary<GamepadButton, InputControl<bool>>();
        public readonly Dictionary<GamepadAxis, InputControl<float>> Axes = new Dictionary<GamepadAxis, InputControl<float>>();
        public GamepadDevice(int deviceId) : base(deviceId, GetGamepadNameSafe(deviceId)) { foreach (GamepadButton button in Enum.GetValues(typeof(GamepadButton))) if (button != GamepadButton.Unknown) Buttons[button] = new InputControl<bool>(); foreach (GamepadAxis axis in Enum.GetValues(typeof(GamepadAxis))) Axes[axis] = new InputControl<float>(); }
        public override void Update() { if (!IsConnected) return; Name = GetGamepadNameSafe(DeviceId); foreach (var pair in Buttons) pair.Value.UpdateState(Raylib.IsGamepadButtonDown(DeviceId, pair.Key)); foreach (var pair in Axes) { float rawValue = Raylib.GetGamepadAxisMovement(DeviceId, pair.Key); pair.Value.UpdateState(Math.Abs(rawValue) > 0.2f ? rawValue : 0f); } }
        public override InputControl<TValue> GetControl<TValue>(string controlName) where TValue : struct { if (typeof(TValue) == typeof(bool) && Enum.TryParse<GamepadButton>(controlName, true, out var button) && Buttons.TryGetValue(button, out var btnControl)) return btnControl as InputControl<TValue>; if (typeof(TValue) == typeof(float) && Enum.TryParse<GamepadAxis>(controlName, true, out var axis) && Axes.TryGetValue(axis, out var axisControl)) return axisControl as InputControl<TValue>; return null; }
        private static string GetGamepadNameSafe(int deviceId)
        {
            string name = "Unknown Gamepad";
            try
            {
                unsafe { name = Marshal.PtrToStringAnsi((IntPtr)Raylib.GetGamepadName(deviceId)); }
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Could not get gamepad name for ID {deviceId}");
            }
            return name ?? "Unknown Gamepad";
        }
    }

    #endregion

    /// <summary>
    /// The main InputSystem class, now with integrated logging and JSON support.
    /// </summary>
    public class InputSystem : SystemClass, IUpdateSys, ILateUpdateSys
    {
        public static event Action<InputDevice> OnDeviceConnected;
        public static event Action<InputDevice> OnDeviceDisconnected;

        private static readonly List<InputActionMap> _actionMaps = new List<InputActionMap>();
        private readonly List<InputDevice> _devices = new List<InputDevice>();
        private readonly bool[] _gamepadStates = new bool[4];

        public static InputAction FindAction(string actionName)
        {
            foreach (var map in _actionMaps)
            {
                if (!map.IsEnabled) continue;
                var action = map.FindAction(actionName);
                if (action != null) return action;
            }
            Logger.Warning($"Could not find action named '{actionName}' in any enabled action map.");
            return null;
        }

        public override void Initialize()
        {
            _devices.Add(new KeyboardDevice());
            LoadActionMapsFromFile("input_actions.json");
            for (int i = 0; i < _gamepadStates.Length; i++)
            {
                if (Raylib.IsGamepadAvailable(i))
                {
                    _gamepadStates[i] = true;
                    ConnectGamepad(i);
                }
            }
            Logger.Info("InputSystem Initialized.");
        }

        public override void Shutdown()
        {
            SaveActionMapsToFile("input_actions.json");
            _devices.Clear();
            _actionMaps.Clear();
            Logger.Info("InputSystem Shutdown.");
        }

        public void Update()
        {
            CheckForDeviceChanges();
            foreach (var device in _devices)
            {
                if (device.IsConnected) device.Update();
            }
            foreach (var map in _actionMaps)
            {
                if (!map.IsEnabled) continue;
                foreach (var action in map.Actions.Values) ProcessAction(action);
            }
        }

        public void LateUpdate() { }

        private void ProcessAction(InputAction action)
        {
            foreach (var binding in action.Bindings)
            {
                var pathParts = binding.Path.Trim('<', '>').Split('/');
                if (pathParts.Length != 2)
                {
                    Logger.Warning($"Invalid binding path '{binding.Path}' for action '{action.Name}'. Skipping.");
                    continue;
                }
                var deviceTypeName = pathParts[0];
                var controlName = pathParts[1];

                foreach (var device in _devices)
                {
                    if (!device.IsConnected || !device.GetType().Name.Equals(deviceTypeName, StringComparison.OrdinalIgnoreCase)) continue;

                    object control = (object)device.GetControl<bool>(controlName) ?? device.GetControl<float>(controlName);
                    if (control == null) continue;

                    var isPressed = (bool)control.GetType().GetProperty("IsPressed").GetValue(control);
                    var wasPressed = (bool)control.GetType().GetProperty("WasPressed").GetValue(control);

                    if (isPressed && !wasPressed)
                    {
                        Logger.Debug($"Action '{action.Name}' Started/Performed by '{controlName}' on '{deviceTypeName}'.");
                        action.TriggerStarted(new CallbackContext(action, control));
                        action.TriggerPerformed(new CallbackContext(action, control));
                    }
                    else if (!isPressed && wasPressed)
                    {
                        Logger.Debug($"Action '{action.Name}' Canceled by '{controlName}' on '{deviceTypeName}'.");
                        action.TriggerCanceled(new CallbackContext(action, control));
                    }
                }
            }
        }

        private void CheckForDeviceChanges()
        {
            for (int i = 0; i < _gamepadStates.Length; i++)
            {
                bool isAvailable = Raylib.IsGamepadAvailable(i);
                if (isAvailable != _gamepadStates[i])
                {
                    _gamepadStates[i] = isAvailable;
                    if (isAvailable) ConnectGamepad(i);
                    else DisconnectGamepad(i);
                }
            }
        }

        private void ConnectGamepad(int deviceId)
        {
            var newDevice = new GamepadDevice(deviceId);
            _devices.Add(newDevice);
            OnDeviceConnected?.Invoke(newDevice);
            Logger.Info($"Gamepad connected: ID {deviceId}, Name: '{newDevice.Name}'");
        }

        private void DisconnectGamepad(int deviceId)
        {
            var device = _devices.FirstOrDefault(d => d is GamepadDevice gd && gd.DeviceId == deviceId);
            if (device != null)
            {
                device.IsConnected = false;
                OnDeviceDisconnected?.Invoke(device);
                Logger.Info($"Gamepad disconnected: ID {deviceId}, Name: '{device.Name}'");
            }
        }
        
        /// <summary>
        /// Loads action maps from a specified JSON file.
        /// </summary>
        /// <param name="filePath">The path to the JSON file.</param>
        private static void LoadActionMapsFromFile(string filePath)
        {
            Logger.Info($"Attempting to load action maps from '{filePath}'.");
            if (!File.Exists(filePath))
            {
                Logger.Warning($"Action map file not found at '{filePath}'. Creating default map and saving.");
                CreateDefaultActionMaps();
                SaveActionMapsToFile(filePath);
                return;
            }

            try
            {
                string json = File.ReadAllText(filePath);
                var dtoCollection = JsonConvert.DeserializeObject<ActionMapCollectionDTO>(json);

                if (dtoCollection == null || dtoCollection.ActionMaps == null)
                {
                     Logger.Error($"Failed to deserialize action maps from '{filePath}'. The file might be empty or malformed.");
                     return;
                }

                _actionMaps.Clear();
                foreach (var mapDto in dtoCollection.ActionMaps)
                {
                    var newMap = new InputActionMap(mapDto.Name);
                    foreach(var actionDto in mapDto.Actions)
                    {
                        var newAction = new InputAction(actionDto.Name);
                        var newBindings = actionDto.Bindings.Select(b => new InputBinding(b, actionDto.Name, actionDto.ControlScheme));
                        newMap.AddAction(newAction, newBindings);
                    }
                    newMap.Enable();
                    _actionMaps.Add(newMap);
                }
                Logger.Info($"Successfully loaded {_actionMaps.Count} action map(s) from '{filePath}'.");
            }
            catch(Exception ex)
            {
                Logger.Error(ex, $"An error occurred while loading or parsing action map file: {filePath}");
                Logger.Warning("Creating default action maps as a fallback.");
                CreateDefaultActionMaps();
            }
        }

        /// <summary>
        /// Saves the current action maps to a specified JSON file.
        /// </summary>
        /// <param name="filePath">The path to save the JSON file to.</param>
        public static void SaveActionMapsToFile(string filePath)
        {
            Logger.Info($"Saving action maps to '{filePath}'.");
            try
            {
                var dtoCollection = new ActionMapCollectionDTO();
                foreach(var map in _actionMaps)
                {
                    var mapDto = new ActionMapDTO { Name = map.Name };
                    foreach(var action in map.Actions.Values)
                    {
                        var actionDto = new ActionDTO
                        {
                            Name = action.Name,
                            ControlScheme = action.Bindings.FirstOrDefault()?.ControlScheme ?? "None", 
                            Bindings = action.Bindings.Select(b => b.Path).ToList()
                        };
                        mapDto.Actions.Add(actionDto);
                    }
                    dtoCollection.ActionMaps.Add(mapDto);
                }

                string json = JsonConvert.SerializeObject(dtoCollection, Formatting.Indented);
                File.WriteAllText(filePath, json);
                Logger.Info($"Successfully saved action maps to '{filePath}'.");
            }
            catch (Exception ex)
            {
                Logger.Error(ex, $"Failed to save action maps to '{filePath}'.");
            }
        }

        /// <summary>
        /// Creates a default set of action maps programmatically. Used as a fallback.
        /// </summary>
        private static void CreateDefaultActionMaps()
        {
            _actionMaps.Clear();
            var gameplayMap = new InputActionMap("Gameplay");
            var jumpAction = new InputAction("Jump");
            gameplayMap.AddAction(jumpAction, new[]
            {
                new InputBinding("<GamepadDevice>/RightFaceDown", "Jump", "Gamepad"),
                new InputBinding("<KeyboardDevice>/Space", "Jump", "Keyboard"),
            });
            gameplayMap.Enable();
            _actionMaps.Add(gameplayMap);
            Logger.Info($"Created default '{gameplayMap.Name}' action map.");
        }
    }
}
