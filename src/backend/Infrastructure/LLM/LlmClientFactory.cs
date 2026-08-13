using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace Infrastructure.LLM;

public class LlmClientFactory
{
    private readonly LlmOptions _options;
    private readonly IConfiguration _configuration;

    public LlmClientFactory(
        IOptions<LlmOptions> options,
        IConfiguration configuration)
    {
        _options = options.Value;
        _configuration = configuration;
    }

    public ILLMClient Create()
    {
        var httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(
                _options.TimeoutSeconds)
        };

        return _options.Provider.ToLowerInvariant() switch
        {
            "gemini" =>
                new GeminiClient(
                    httpClient,
                    Options.Create(_options),
                    _configuration),

            "cerebras" =>
                new CerebrasClient(
                    httpClient,
                    Options.Create(_options),
                    _configuration),

            _ =>
                throw new InvalidOperationException(
                    $"Unsupported LLM provider: {_options.Provider}")
        };
    }
}