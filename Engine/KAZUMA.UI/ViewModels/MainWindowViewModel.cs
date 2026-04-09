using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using KAZUMA.Orchestrator;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace KAZUMA.UI.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly KazumaOrchestrator _orchestrator;

    [ObservableProperty]
    private string _inputText = string.Empty;

    [ObservableProperty]
    private bool _isBusy = false;

    public ObservableCollection<string> ChatMessages { get; } = new();

    public MainWindowViewModel(KazumaOrchestrator orchestrator)
    {
        _orchestrator = orchestrator;
        ChatMessages.Add("K.A.Z.U.M.A: Gotowy do dzia┼éania.");
    }

    [RelayCommand]
    private async Task SendMessageAsync()
    {
        if (string.IsNullOrWhiteSpace(InputText)) return;

        var userText = InputText;
        InputText = string.Empty;
        ChatMessages.Add($"Ty: {userText}");
        IsBusy = true;

        try
        {
            var response = await _orchestrator.ProcessTextAsync(userText);
            ChatMessages.Add($"K.A.Z.U.M.A: {response}");
        }
        catch (System.Exception ex)
        {
            ChatMessages.Add($"B┼é─ůd: {ex.Message}");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task StartVoiceAsync()
    {
        ChatMessages.Add("System: Rozpoczynam nas┼éuchiwanie...");
        await Task.CompletedTask;
    }
}
