namespace KAZUMA.Core.Interfaces;

public interface ISpeechRecognition
{
    Task<string> TranscribeAsync(string audioFile);
}
