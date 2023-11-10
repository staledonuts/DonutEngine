namespace Engine.Systems.UI;
using ImGuiNET;
using System.Numerics;
using Raylib_cs;
using Engine.Systems.Audio;
using Engine.Logging;

public class Console : DocumentWindow
{
    private Dictionary<string, Action<string>> commands = new Dictionary<string, Action<string>>();
    private List<string> commandHistory = new List<string>();
    private int commandIndex = -1;
    private string inputBuffer = "";

    public override void Setup()
    {
        commands["clear"] = _ => DonutLogging.logs.Clear();
        commands["stopmusic"] = _ => EngineSystems.GetSystem<AudioControl>().StopMusic();
        commands["playmusic"] = args => EngineSystems.GetSystem<AudioControl>().PlayMusic(args.Trim());
        commands["playsfx"] = args => EngineSystems.GetSystem<AudioControl>().PlaySFX(args.Trim());
        commands["stopsfx"] = args => EngineSystems.GetSystem<AudioControl>().StopSFX(args.Trim());
        commands["musicvolume"] = args =>
        {
            if (float.TryParse(args, out float volume))
            {
                EngineSystems.GetSystem<AudioControl>().SetMusicVolume(volume);
            }
            else
            {
                Raylib.TraceLog(TraceLogLevel.LOG_ERROR, $"Invalid volume: {args}");
            }
        };
    }
    public override void Show()
    {
        
        ImGui.PushStyleVar(ImGuiStyleVar.WindowPadding, new Vector2(0, 0));
        ImGui.SetNextWindowSizeConstraints(new Vector2(400, 400), new Vector2((float)Raylib.GetScreenWidth(), (float)Raylib.GetScreenHeight()));
        
        if (ImGui.Begin("Console", ref Open, ImGuiWindowFlags.AlwaysUseWindowPadding | ImGuiWindowFlags.NoCollapse))
        {
            Focused = ImGui.IsWindowFocused(ImGuiFocusedFlags.RootAndChildWindows);
            Vector2 size = ImGui.GetContentRegionAvail();
            Window();
            ImGui.End();
        }
        ImGui.PopStyleVar();
    }
    public override void Shutdown()
    {
        
    }
    private void Window()
    {
        if (ImGui.BeginChildFrame(1, new Vector2(ImGui.GetWindowContentRegionMax().X, ImGui.GetWindowContentRegionMax().Y - 60), ImGuiWindowFlags.AlwaysVerticalScrollbar | ImGuiWindowFlags.HorizontalScrollbar))
        {
            bool scrollToBottom = false;
            foreach (string log in DonutLogging.logs)
            {
                ImGui.TextUnformatted(log);
            }

            // Scroll to the bottom if a command was just executed
            if (!string.IsNullOrEmpty(inputBuffer))
            {
                scrollToBottom = true;
            }
            
            if (scrollToBottom)
            {
                ImGui.SetScrollY(ImGui.GetScrollMaxY());
            }

            ImGui.EndChildFrame();
        }

        // Draw the input field
        if (ImGui.InputText("##Input", ref inputBuffer, 256, ImGuiInputTextFlags.EnterReturnsTrue | ImGuiInputTextFlags.AllowTabInput))
        {
            ExecuteCommand(inputBuffer);
            inputBuffer = "";
        }
    }

    private void ExecuteCommand(string commandInput)
    {
        var parts = commandInput.Split(new[] { ' ' }, 2);
        var command = parts[0];
        var args = parts.Length > 1 ? parts[1] : "";

        if (commands.TryGetValue(command, out var action))
        {
            action.Invoke(args);
            commandHistory.Add(commandInput);
            commandIndex = commandHistory.Count;
        }
        else
        {
            Raylib.TraceLog(TraceLogLevel.LOG_ERROR, $"Unknown command: {command}");
        }
    }

    private void HistoryInput()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.KEY_UP))
        {
            InputCommand(-1);
        }
        else if (Raylib.IsKeyPressed(KeyboardKey.KEY_DOWN))
        {
            InputCommand(1);
        }
    }

    private void InputCommand(int direction)
    {
        // Update the command index based on the direction
        commandIndex += direction;

        // Make sure the command index is within the bounds of the command history
        commandIndex = Math.Max(0, Math.Min(commandIndex, commandHistory.Count - 1));

        // Set the input buffer to the command from the history
        if (commandHistory.Count > 0)
        {
            inputBuffer = commandHistory[commandIndex];
        }
    }
}