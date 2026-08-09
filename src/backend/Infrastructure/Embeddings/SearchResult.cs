using Infrastructure.Documents;

namespace Infrastructure.Embeddings;

public class SearchResult
{
    public DocumentChunk Chunk { get; set; } = default!;

    public double Similarity { get; set; }
}