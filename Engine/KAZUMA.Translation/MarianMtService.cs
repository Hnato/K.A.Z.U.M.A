using System.Net.Http.Json;
using KAZUMA.Core.Interfaces;
using KAZUMA.Core.DTO;

namespace KAZUMA.Translation;

public class MarianMtService : ITranslationService
{
    private readonly HttpClient _http;

    public MarianMtService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> TranslateAsync(string text, string sourceLang, string targetLang)
    {
        var response = await _http.PostAsJsonAsync(
            "http://localhost:5001/translate",
            new { text, source = sourceLang, target = targetLang });

        var result = await response.Content.ReadFromJsonAsync<TranslationResponse>();
        return result?.Translated ?? string.Empty;
    }
}
