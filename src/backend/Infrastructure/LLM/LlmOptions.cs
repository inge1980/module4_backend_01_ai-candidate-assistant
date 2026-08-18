namespace Infrastructure.LLM;

public class LlmOptions
{
    public string Provider { get; init; } = "Gemini";

    public int MaxOutputTokens { get; init; } = 500;

    public int TimeoutSeconds { get; init; } = 20;

    public string ThinkingLevel { get; init; } = "minimal";

    public GeminiOptions Gemini { get; init; } = new();

    public GroqOptions Groq { get; init; } = new();

    public OpenRouterOptions OpenRouter { get; init; } = new();

    public CerebrasOptions Cerebras { get; init; } = new();

}

public class GeminiOptions
{
    public string Model { get; init; } = "gemini-3.6-flash";

    public int TimeoutSeconds { get; init; } = 20;
}

public class GroqOptions
{
    public string Model { get; init; } = "openai/gpt-oss-120b";

    public int TimeoutSeconds { get; init; } = 20;
}

public class OpenRouterOptions
{
    public string Model { get; init; } = "openai/gpt-oss-120b";

    public int TimeoutSeconds { get; init; } = 20;
}

public class CerebrasOptions
{
    public string Model { get; init; } = "gpt-oss-120b";

    public int TimeoutSeconds { get; init; } = 20;
}