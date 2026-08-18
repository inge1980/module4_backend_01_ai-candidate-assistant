using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Http;

namespace Infrastructure.LLM;

public class LlmClientFactory
{
    private readonly LlmOptions _options;
    private readonly IConfiguration _configuration;
    private readonly IHttpClientFactory _httpClientFactory;

    public LlmClientFactory(
        IOptions<LlmOptions> options,
        IConfiguration configuration,
        IHttpClientFactory httpClientFactory)
    {
        _options = options.Value;
        _configuration = configuration;
        _httpClientFactory = httpClientFactory;
    }

    public ILLMClient Create()
    {
        var clients = new List<ILLMClient>();

        var provider =
            _options.Provider
                .Trim()
                .ToLowerInvariant();

        switch (provider)
        {
            case "gemini":
                clients.Add(CreateGemini());
                clients.Add(CreateGroq());
                clients.Add(CreateOpenRouter());
                break;

            case "groq":
                clients.Add(CreateGroq());
                clients.Add(CreateOpenRouter());
                clients.Add(CreateGemini());
                break;

            case "openrouter":
                clients.Add(CreateOpenRouter());
                clients.Add(CreateGroq());
                clients.Add(CreateGemini());
                break;
            default:
                throw new InvalidOperationException($"Unsupported LLM provider: {_options.Provider}");
        }
        return new FallbackLlmClient(clients);
    }

    private GeminiClient CreateGemini()
    {
        var httpClient =
            _httpClientFactory.CreateClient(
                "Gemini");

        httpClient.Timeout =
            TimeSpan.FromSeconds(
                _options.Gemini.TimeoutSeconds);

        return new GeminiClient(
            httpClient,
            Options.Create(_options),
            _configuration);
    }

    private GroqClient CreateGroq()
    {
        var httpClient =
            _httpClientFactory.CreateClient(
                "Groq");

        httpClient.Timeout =
            TimeSpan.FromSeconds(
                _options.Groq.TimeoutSeconds);

        return new GroqClient(
            httpClient,
            Options.Create(_options),
            _configuration);
    }

    private OpenRouterClient CreateOpenRouter()
    {
        var httpClient =
            _httpClientFactory.CreateClient("OpenRouter");

        httpClient.Timeout =
            TimeSpan.FromSeconds(
                _options.OpenRouter.TimeoutSeconds);

        return new OpenRouterClient(
            httpClient,
            Options.Create(_options),
            _configuration);
    }
    
    private CerebrasClient CreateCerebras()
    {
        var httpClient =
            _httpClientFactory.CreateClient(
                "Cerebras");

        httpClient.Timeout =
            TimeSpan.FromSeconds(
                _options.Cerebras.TimeoutSeconds);

        return new CerebrasClient(
            httpClient,
            Options.Create(_options),
            _configuration);
    }
}