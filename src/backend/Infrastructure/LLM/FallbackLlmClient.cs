using System.Diagnostics;

namespace Infrastructure.LLM;

public sealed class FallbackLlmClient : ILLMClient
{
    private readonly IReadOnlyList<ILLMClient> _clients;

    public FallbackLlmClient(
        IEnumerable<ILLMClient> clients)
    {
        _clients = clients.ToList();
        if (_clients.Count == 0)
        {
            throw new InvalidOperationException(
                "At least one LLM client must be configured.");
        }
    }

    public async Task<string> GenerateAsync(
        string prompt,
        CancellationToken cancellationToken = default)
    {
        Exception? lastException = null;

        foreach (var client in _clients)
        {
            var provider =
                client.GetType().Name.Replace(
                    "Client",
                    string.Empty);

            var stopwatch = Stopwatch.StartNew();
            try
            {
                Console.WriteLine($"[LLM] Trying provider: {provider}");

                var result =
                    await client.GenerateAsync(
                        prompt,
                        cancellationToken);

                stopwatch.Stop();
                Console.WriteLine($"[LLM] Provider succeeded: {provider} ({stopwatch.ElapsedMilliseconds} ms)");
                return result;
            }
            catch (LlmProviderException ex)
            {
                stopwatch.Stop();
                Console.WriteLine($"[LLM] Provider failed: {provider} ({stopwatch.ElapsedMilliseconds} ms) Status={ex.StatusCode} Transient={ex.IsTransient}");
                lastException = ex;
                Console.WriteLine($"[LLM] Falling back from {provider}.");
            }
        }

        throw new InvalidOperationException(
            "All configured LLM providers failed.",
            lastException);
    }
}