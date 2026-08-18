using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using Infrastructure.LLM;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.LLM;

public class GeminiClient : ILLMClient
{
    private readonly HttpClient _httpClient;
    private readonly LlmOptions _options;
    private readonly IConfiguration _configuration;

    public GeminiClient(
        HttpClient httpClient,
        IOptions<LlmOptions> options,
        IConfiguration configuration)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _configuration = configuration;
    }

    public async Task<string> GenerateAsync(
        string prompt,
        CancellationToken cancellationToken = default)
    {
        var apiKey =
            _configuration["Gemini:ApiKey"];

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new InvalidOperationException(
                "Gemini API key is missing.");
        }

        if (string.IsNullOrWhiteSpace(_options.Model))
        {
            throw new InvalidOperationException(
                "Gemini model is missing.");
        }

        var baseUrl =
            _configuration["Gemini:BaseUrl"]
            ?? throw new InvalidOperationException(
                "Gemini base URL is missing.");

        var apiVersion =
            _configuration["Gemini:ApiVersion"]
            ?? throw new InvalidOperationException(
                "Gemini API version is missing.");

        var url =
            $"{baseUrl}/" +
            $"{apiVersion}/models/" +
            $"{_options.Model}:generateContent" +
            $"?key={apiKey}";

        var request = new
        {
            contents = new[]
            {
                new
                {
                    parts = new[]
                    {
                        new
                        {
                            text = prompt
                        }
                    }
                }
            },
            generationConfig = new
            {
                maxOutputTokens = _options.MaxOutputTokens
            }
        };

        var httpStopwatch = Stopwatch.StartNew();
        using var response =
            await _httpClient.PostAsJsonAsync(
                url,
                request,
                cancellationToken);
        httpStopwatch.Stop();
        Console.WriteLine($"[Timing] Gemini HTTP request: {httpStopwatch.ElapsedMilliseconds} ms");

        var readStopwatch = Stopwatch.StartNew();
        var responseBody =
            await response.Content.ReadAsStringAsync(
                cancellationToken);
        readStopwatch.Stop();
        Console.WriteLine($"[Timing] Gemini response read: {readStopwatch.ElapsedMilliseconds} ms");

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"Gemini request failed ({(int)response.StatusCode}): {responseBody}");
        }

        var parseStopwatch = Stopwatch.StartNew();
        using var json =
            JsonDocument.Parse(responseBody);
        var text =
            json.RootElement
                .GetProperty("candidates")[0]
                .GetProperty("content")
                .GetProperty("parts")[0]
                .GetProperty("text")
                .GetString();
        parseStopwatch.Stop();
        Console.WriteLine($"[Timing] Gemini JSON parsing: {parseStopwatch.ElapsedMilliseconds} ms");

        return text
            ?? throw new InvalidOperationException(
                "Gemini returned an empty response.");
    }
}