using System.Diagnostics;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.LLM;

public class GeminiClient : ILLMClient
{
    private readonly HttpClient _httpClient;
    private readonly LlmOptions _options;
    private readonly IConfiguration _configuration;
    private readonly string _model;
    public string Provider => "Gemini";
    public string Model => _model;

    public GeminiClient(
        HttpClient httpClient,
        IOptions<LlmOptions> options,
        IConfiguration configuration,
        string model)
    {
        _httpClient = httpClient;
        _options = options.Value;
        _configuration = configuration;
        _model = model;
    }

    public async Task<string> GenerateAsync(
        string prompt,
        CancellationToken cancellationToken = default)
    {
        var apiKey = _configuration["Gemini:ApiKey"];

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new LlmProviderException(
                "Gemini",
                "Gemini API key is missing.");
        }

        if (string.IsNullOrWhiteSpace(_model))
        {
            throw new LlmProviderException(
                "Gemini",
                "Gemini model is missing.");
        }

        var baseUrl =
            _configuration["Gemini:BaseUrl"]
            ?? throw new LlmProviderException(
                "Gemini",
                "Gemini base URL is missing.");

        var apiVersion =
            _configuration["Gemini:ApiVersion"]
            ?? throw new LlmProviderException(
                "Gemini",
                "Gemini API version is missing.");

        var url =
            $"{baseUrl}/" +
            $"{apiVersion}/models/" +
            $"{_model}:generateContent" +
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
                maxOutputTokens = _options.MaxOutputTokens,
                thinkingConfig = new
                {
                    thinkingLevel = _options.ThinkingLevel
                }
            }
        };

        using var httpRequest = new HttpRequestMessage(
            HttpMethod.Post,
            url)
        {
            Content = JsonContent.Create(request)
        };

        var stopwatch = Stopwatch.StartNew();

        try
        {
            using var response =
                await _httpClient.SendAsync(
                    httpRequest,
                    HttpCompletionOption.ResponseHeadersRead,
                    cancellationToken);

            stopwatch.Stop();

            Console.WriteLine($"[LLM] Gemini HTTP: {stopwatch.ElapsedMilliseconds} ms");

            var responseBody =
                await response.Content.ReadAsStringAsync(
                    cancellationToken);

            if (!response.IsSuccessStatusCode)
            {
                var statusCode =
                    (int)response.StatusCode;

                var transient =
                    statusCode == 408 ||
                    statusCode == 429 ||
                    statusCode >= 500;

                throw new LlmProviderException(
                    "Gemini",
                    $"Gemini request failed ({statusCode}): {responseBody}",
                    statusCode,
                    transient);
            }

            using var json = JsonDocument.Parse(responseBody);

            var text =
                json.RootElement
                    .GetProperty("candidates")[0]
                    .GetProperty("content")
                    .GetProperty("parts")[0]
                    .GetProperty("text")
                    .GetString();

            return text
                ?? throw new LlmProviderException(
                    "Gemini",
                    "Gemini returned an empty response.");
        }
        catch (LlmProviderException)
        {
            throw;
        }
        catch (TaskCanceledException ex)
            when (!cancellationToken.IsCancellationRequested)
        {
            stopwatch.Stop();

            throw new LlmProviderException(
                "Gemini",
                $"Gemini request timed out after {stopwatch.ElapsedMilliseconds} ms.",
                isTransient: true,
                innerException: ex);
        }
        catch (HttpRequestException ex)
        {
            stopwatch.Stop();

            throw new LlmProviderException(
                "Gemini",
                "Gemini network request failed.",
                isTransient: true,
                innerException: ex);
        }
    }
}