namespace KAZUMA.Core.DTO;

public class ChatCompletionRequest
{
    public string Model { get; set; } = string.Empty;
    public List<ChatMessageDto> Messages { get; set; } = new();
    public bool Stream { get; set; } = false;
}

public class ChatMessageDto
{
    public string Role { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
}

public class ChatCompletionResponse
{
    public List<Choice> Choices { get; set; } = new();
}

public class Choice
{
    public ChatMessageDto Message { get; set; } = new();
}
