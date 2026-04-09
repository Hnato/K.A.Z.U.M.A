namespace KAZUMA.Core.Interfaces;

public interface ILlmService
{
    Task<string> GenerateAsync(string prompt);
}
