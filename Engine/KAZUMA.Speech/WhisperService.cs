using System.Net.Http.Json;
using KAZUMA.Core.Interfaces;

namespace KAZUMA.Speech;

public class WhisperService : ISpeechRecognition
{
    private readonly HttpClient _http;

    public WhisperService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> TranscribeAsync(string audioFile)
    {
        var response = await _http.PostAsJsonAsync("http://localhost:5002/transcribe", new { file = audioFile });
        var result = await response.Content.ReadFromJsonAsync<TranscriptionResponse>();
        return result?.Text ?? string.Empty;
    }

    private class TranscriptionResponse { public string Text { get; set; } = ""; }
}
