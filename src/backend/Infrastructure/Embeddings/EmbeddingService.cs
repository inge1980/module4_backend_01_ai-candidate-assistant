// Text to embedding
using System.Net.Http.Json;

namespace Infrastructure.Embeddings;

public class EmbeddingService
{
    private readonly HttpClient _httpClient;

    private const string Model =
        "qllama/bge-small-en-v1.5:latest";

    private const string OllamaUrl =
        "http://localhost:11434/api/embed";

    public EmbeddingService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<float[]> Create(string text)
    {
        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException(
                "Text cannot be empty.",
                nameof(text));
        }

        var request = new
        {
            model = Model,
            input = text
        };

        var response =
            await _httpClient.PostAsJsonAsync(
                OllamaUrl,
                request);

        response.EnsureSuccessStatusCode();

        var result =
            await response.Content
                .ReadFromJsonAsync<OllamaEmbeddingResponse>();

        if (result?.Embeddings == null ||
            result.Embeddings.Length == 0)
        {
            throw new InvalidOperationException(
                "Ollama returned no embedding.");
        }

        var embedding = result.Embeddings[0];

        if (embedding.Length != 384)
        {
            throw new InvalidOperationException(
                $"Expected a 384-dimensional embedding, " +
                $"but Ollama returned {embedding.Length} dimensions.");
        }

        return embedding;
    }

    private sealed class OllamaEmbeddingResponse
    {
        public float[][] Embeddings { get; set; } = [];
    }
}