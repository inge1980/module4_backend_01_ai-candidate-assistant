using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.LLM;

public class CerebrasClient : ILLMClient
{
    private readonly HttpClient _httpClient;
    private readonly LlmOptions _options;
    private readonly IConfiguration _configuration;

    public CerebrasClient(
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
            _configuration["Cerebras:ApiKey"];

        if (string.IsNullOrWhiteSpace(apiKey))
        {
            throw new InvalidOperationException(
                "Cerebras API key is missing.");
        }

        if (string.IsNullOrWhiteSpace(_options.Model))
        {
            throw new InvalidOperationException(
                "Cerebras model is missing.");
        }

        var baseUrl =
            _configuration["Cerebras:BaseUrl"]
            ?? throw new InvalidOperationException(
                "Cerebras base URL is missing.");

        var request =
            new HttpRequestMessage(
                HttpMethod.Post,
                $"{baseUrl}/chat/completions");

        request.Headers.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                apiKey);

        request.Content =
            JsonContent.Create(
                new
                {
                    model = _options.Model,
                    messages = new[]
                    {
                        new
                        {
                            role = "user",
                            content = prompt
                        }
                    },
                    max_tokens = _options.MaxOutputTokens
                });

        using var response =
            await _httpClient.SendAsync(
                request,
                cancellationToken);

        var responseBody =
            await response.Content.ReadAsStringAsync(
                cancellationToken);

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException(
                $"Cerebras request failed ({(int)response.StatusCode}): {responseBody}");
        }

        using var json =
            JsonDocument.Parse(responseBody);

        var text =
            json.RootElement
                .GetProperty("choices")[0]
                .GetProperty("message")
                .GetProperty("content")
                .GetString();

        return text
            ?? throw new InvalidOperationException(
                "Cerebras returned an empty response.");
    }
}