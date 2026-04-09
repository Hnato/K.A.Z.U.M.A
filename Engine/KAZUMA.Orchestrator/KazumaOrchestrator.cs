using KAZUMA.Core.Interfaces;

namespace KAZUMA.Orchestrator;

public class KazumaOrchestrator
{
    private readonly ITranslationService _translator;
    private readonly ILlmService _llm;
    private readonly ISpeechRecognition _stt;
    private readonly ITtsService _tts;
    private readonly IToolExecutor _tools;

    public KazumaOrchestrator(
        ITranslationService translator,
        ILlmService llm,
        ISpeechRecognition stt,
        ITtsService tts,
        IToolExecutor tools)
    {
        _translator = translator;
        _llm = llm;
        _stt = stt;
        _tts = tts;
        _tools = tools;
    }

    public async Task<string> ProcessVoiceAsync(string audioFile)
    {
        // 1. STT (Rozpoznawanie mowy)
        var textPl = await _stt.TranscribeAsync(audioFile);

        return await ProcessTextAsync(textPl);
    }

    public async Task<string> ProcessTextAsync(string textPl)
    {
        // 2. T┼éumaczenie PL -> EN
        var textEn = await _translator.TranslateAsync(textPl, "pl", "en");

        // 3. LLM reasoning (Z instrukcj─ů sterowania systemem)
        var systemPrompt = "Jeste┼Ť K.A.Z.U.M.A. Mo┼╝esz sterowa─ç systemem u┼╝ywaj─ůc tag├│w: [CLICK_LEFT], [MOVE_MOUSE X Y], [TYPE Tekst], [PRESS_KEY Klucz].";
        var fullPrompt = $"{systemPrompt}\n\nU┼╝ytkownik: {textEn}";
        var responseEn = await _llm.GenerateAsync(fullPrompt);

        // 4. Detekcja i wykonanie komend
        await ParseAndExecuteCommandsAsync(responseEn);

        // 5. T┼éumaczenie odpowiedzi na PL (usuwamy tagi komend z odpowiedzi dla u┼╝ytkownika)
        var cleanResponseEn = System.Text.RegularExpressions.Regex.Replace(responseEn, @"\[.*?\]", "").Trim();
        var responsePl = await _translator.TranslateAsync(cleanResponseEn, "en", "pl");

        // 6. TTS
        await _tts.SpeakAsync(responsePl);

        return responsePl;
    }

    private async Task ParseAndExecuteCommandsAsync(string response)
    {
        var matches = System.Text.RegularExpressions.Regex.Matches(response, @"\[(.*?)\]");
        foreach (System.Text.RegularExpressions.Match match in matches)
        {
            var command = match.Groups[1].Value;
            await _tools.ExecuteAsync(command);
        }
    }

    public async Task ExecuteCommandAsync(string command)
    {
        await _tools.ExecuteAsync(command);
    }
}
