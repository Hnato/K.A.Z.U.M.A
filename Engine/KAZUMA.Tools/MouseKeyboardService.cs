using KAZUMA.Core.Interfaces;
using WindowsInput;
using WindowsInput.Native;

namespace KAZUMA.Tools;

public class MouseKeyboardService : IToolExecutor
{
    private readonly InputSimulator _simulator;

    public MouseKeyboardService()
    {
        _simulator = new InputSimulator();
    }

    public async Task ExecuteAsync(string command)
    {
        // Simple command parsing: "CLICK_LEFT", "MOVE_MOUSE X Y", "TYPE Text"
        var parts = command.Split(' ');
        var action = parts[0].ToUpper();

        switch (action)
        {
            case "CLICK_LEFT":
                _simulator.Mouse.LeftButtonClick();
                break;
            case "CLICK_RIGHT":
                _simulator.Mouse.RightButtonClick();
                break;
            case "MOVE_MOUSE":
                if (parts.Length >= 3 && int.TryParse(parts[1], out int x) && int.TryParse(parts[2], out int y))
                {
                    // Convert screen coordinates if needed, but InputSimulator uses 0-65535
                    _simulator.Mouse.MoveMouseTo(x, y);
                }
                break;
            case "TYPE":
                var text = string.Join(" ", parts.Skip(1));
                _simulator.Keyboard.TextEntry(text);
                break;
            case "PRESS_KEY":
                if (parts.Length >= 2 && Enum.TryParse(parts[1], out VirtualKeyCode key))
                {
                    _simulator.Keyboard.KeyPress(key);
                }
                break;
            default:
                // Log or handle unknown command
                break;
        }

        await Task.CompletedTask;
    }
}
