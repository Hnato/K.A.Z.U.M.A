namespace KAZUMA.Core.Interfaces;

public interface ITtsService
{
    Task SpeakAsync(string text);
}
