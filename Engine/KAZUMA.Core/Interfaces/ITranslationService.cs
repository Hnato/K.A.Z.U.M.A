namespace KAZUMA.Core.Interfaces;

public interface ITranslationService
{
    Task<string> TranslateAsync(string text, string sourceLang, string targetLang);
}
