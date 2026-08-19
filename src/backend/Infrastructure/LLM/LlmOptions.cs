namespace Infrastructure.LLM;

public class LlmOptions
{
    public int MaxOutputTokens { get; init; } = 500;

    public string ThinkingLevel { get; init; } = "minimal";

    public List<LlmProviderOptions> Providers { get; init; } = [];
}