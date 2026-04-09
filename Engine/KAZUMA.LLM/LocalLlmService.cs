using System.Net.Http.Json;
using KAZUMA.Core.Interfaces;
using KAZUMA.Core.DTO;

namespace KAZUMA.LLM;

public class LocalLlmService : ILlmService
{
    private readonly HttpClient _http;
    private const string BaseUrl = "http://127.0.0.1:1234/v1/chat/completions";

    public LocalLlmService(HttpClient http)
    {
        _http = http;
    }

    public async Task<string> GenerateAsync(string prompt)
    {
        var request = new ChatCompletionRequest
        {
            Model = "google/gemma-3-12b",
            Messages = new List<ChatMessageDto>
            {
                new ChatMessageDto { Role = "system", Content = "Jeste┼Ť K.A.Z.U.M.A, asystentem systemowym z pe┼éno kontrol─ů nad komputerem. Odpowiadaj kr├│tko i konkretnie." },
                new ChatMessageDto { Role = "user", Content = prompt }
            },
            Stream = false
        };

        try
        {
            var response = await _http.PostAsJsonAsync(BaseUrl, request);
            response.EnsureSuccessStatusCode();
            
            var result = await response.Content.ReadFromJsonAsync<ChatCompletionResponse>();
            return result?.Choices.FirstOrDefault()?.Message.Content ?? string.Empty;
        }
        catch (Exception ex)
        {
            return $"Error connecting to LLM: {ex.Message}";
        }
    }
}
