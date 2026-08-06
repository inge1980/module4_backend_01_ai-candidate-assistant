using OpenAI.Embeddings;


namespace Infrastructure.Embeddings;


public class EmbeddingService
{

    private readonly EmbeddingClient _client;


    public EmbeddingService()
    {
        _client = new EmbeddingClient(
            "text-embedding-3-small",
            Environment.GetEnvironmentVariable("OPENAI_API_KEY"));
    }


    public async Task<float[]> Create(
        string text)
    {

        var result =
            await _client.GenerateEmbeddingAsync(text);


        return result.Value.ToFloats().ToArray();
    }
}