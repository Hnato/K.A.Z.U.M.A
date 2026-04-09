using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using KAZUMA.UI.ViewModels;
using KAZUMA.UI.Views;
using Microsoft.Extensions.DependencyInjection;
using KAZUMA.Core.Interfaces;
using KAZUMA.LLM;
using KAZUMA.Translation;
using KAZUMA.Speech;
using KAZUMA.TTS;
using KAZUMA.Tools;
using KAZUMA.Orchestrator;
using System;

namespace KAZUMA.UI;

public partial class App : Application
{
    public IServiceProvider? Services { get; private set; }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var serviceCollection = new ServiceCollection();
        ConfigureServices(serviceCollection);
        Services = serviceCollection.BuildServiceProvider();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = Services.GetRequiredService<MainWindowViewModel>(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddHttpClient();
        
        // Services
        services.AddSingleton<ITranslationService, MarianMtService>();
        services.AddSingleton<ILlmService, LocalLlmService>();
        services.AddSingleton<ISpeechRecognition, WhisperService>();
        services.AddSingleton<ITtsService, PiperService>();
        services.AddSingleton<IToolExecutor, MouseKeyboardService>();
        
        // Orchestrator
        services.AddSingleton<KazumaOrchestrator>();
        
        // ViewModels
        services.AddSingleton<MainWindowViewModel>();
    }
}
