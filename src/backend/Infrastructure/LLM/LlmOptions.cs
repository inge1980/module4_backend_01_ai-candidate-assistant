namespace Infrastructure.LLM;

public class LlmOptions
{
    public string Provider { get; init; } = string.Empty;
    public string Model { get; init; } = string.Empty;
    public int MaxOutputTokens { get; init; } = 1500;
    public int TimeoutSeconds { get; init; } = 60;
}