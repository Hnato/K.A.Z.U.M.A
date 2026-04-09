using System.Net.Http.Json;
using KAZUMA.Core.Interfaces;

namespace KAZUMA.TTS;

public class PiperService : ITtsService
{
    private readonly HttpClient _http;

    public PiperService(HttpClient http)
    {
        _http = http;
    }

    public async Task SpeakAsync(string text)
    {
        await _http.PostAsJsonAsync("http://localhost:5003/speak", new { text });
    }
}
