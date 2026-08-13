namespace Infrastructure.LLM;

public interface ILLMClient
{
    Task<string> GenerateAsync(
        string prompt,
        CancellationToken cancellationToken = default);
}