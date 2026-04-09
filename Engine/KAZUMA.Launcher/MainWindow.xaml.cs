using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace KAZUMA.Launcher
{
    public partial class MainWindow : Window
    {
        private string _logPath;
        private string _supportDir;
        private string _modelName = "google/gemma-3-12b";

        public MainWindow()
        {
            InitializeComponent();
            _supportDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Engine");
            _logPath = Path.Combine(_supportDir, "Logs", $"kazuma_{DateTime.Now:yyyyMMdd_HHmmss}.log");
            
            if (!Directory.Exists(Path.Combine(_supportDir, "Logs")))
                Directory.CreateDirectory(Path.Combine(_supportDir, "Logs"));

            LoadConfig();
            Task.Run(() => RunLauncherProcess());
        }

        private void LoadConfig()
        {
            try
            {
                var configPath = Path.Combine(_supportDir, "model_config.txt");
                if (File.Exists(configPath))
                    _modelName = File.ReadAllText(configPath).Trim();
            }
            catch { /* fallback to default */ }
        }

        private async Task RunLauncherProcess()
        {
            Log("Uruchamianie K.A.Z.U.M.A Launcher...");
            UpdateStatus("Sprawdzanie LM Studio...");

            if (!IsProcessRunning("LM Studio"))
            {
                Log("LM Studio nie dzia┼éa. Pr├│ba uruchomienia...");
                if (!StartLMStudio())
                {
                    Log("Nie znaleziono LM Studio w domy┼Ťlnych ┼Ťcie┼╝kach.");
                    UpdateStatus("B┼é─ůd: Nie znaleziono LM Studio");
                    return;
                }
                Log("LM Studio uruchomione.");
            }
            else
            {
                Log("LM Studio ju┼╝ dzia┼éa.");
            }

            UpdateStatus("┼üadowanie modelu...");
            Log($"┼üadowanie modelu: {_modelName}");
            
            if (await LoadModel(_modelName))
            {
                UpdateStatus("Model gotowy");
                Log("Model za┼éadowany pomy┼Ťlnie.");
            }
            else
            {
                UpdateStatus("B┼é─ůd ┼éadowania modelu");
                Log("B┼é─ůd podczas ┼éadowania modelu via API.");
            }
        }

        private bool IsProcessRunning(string name)
        {
            return Process.GetProcessesByName(name).Any();
        }

        private bool StartLMStudio()
        {
            // Standard paths for LM Studio
            string[] commonPaths = {
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Programs", "lmstudio", "LM Studio.exe"),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "LM Studio", "LM Studio.exe")
            };

            foreach (var path in commonPaths)
            {
                if (File.Exists(path))
                {
                    Process.Start(path);
                    return true;
                }
            }
            return false;
        }

        private async Task<bool> LoadModel(string model)
        {
            using var client = new HttpClient();
            client.Timeout = TimeSpan.FromSeconds(30);
            
            try
            {
                // Wait for LM Studio API to be ready (up to 30s)
                for (int i = 0; i < 6; i++)
                {
                    try
                    {
                        var response = await client.PostAsJsonAsync("http://localhost:1234/api/v1/models/load", new { model_id = model });
                        return response.IsSuccessStatusCode;
                    }
                    catch
                    {
                        await Task.Delay(5000);
                    }
                }
            }
            catch (Exception ex)
            {
                Log($"Wyj─ůtek podczas ┼éadowania modelu: {ex.Message}");
            }
            return false;
        }

        private void Log(string message)
        {
            var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {message}{Environment.NewLine}";
            File.AppendAllText(_logPath, logEntry);
        }

        private void UpdateStatus(string status)
        {
            Dispatcher.Invoke(() => StatusText.Text = status);
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
