using Infrastructure.Embeddings;
using Infrastructure.Reranking;

namespace Application.Knowledge;

public sealed class KnowledgeRetrievalService(
    EmbeddingService embeddingService,
    VectorStore vectorStore,
    MetadataEvidenceScorer evidenceScorer)
    : IKnowledgeRetrievalService
{
    public async Task<KnowledgeRetrievalResult> RetrieveAsync(
        string query,
        int retrievalLimit = 10,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(query))
        {
            throw new ArgumentException(
                "Query cannot be empty.",
                nameof(query));
        }

        if (retrievalLimit <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(retrievalLimit),
                "Retrieval limit must be greater than zero.");
        }

        // Console.WriteLine("Generating query embedding...");
        var embedding =
            await embeddingService.Create(query);
        // Console.WriteLine($"Embedding dimensions: {embedding.Length}");

        // Console.WriteLine();
        // Console.WriteLine("Searching PostgreSQL...");
        var results =
            await vectorStore.SearchAsync(
                embedding,
                limit: retrievalLimit);
        foreach (var result in results)
        {
            evidenceScorer.Score(query, result);
        }

        var rankedResults =
            results
                .OrderByDescending(
                    result => result.CombinedScore)
                .Select(
                    result => new KnowledgeRetrievalItem(
                        Source: result.Chunk.Source,
                        Heading: result.Chunk.HeadingPath,
                        SemanticType: result.Chunk.SemanticType,
                        Content: result.Chunk.Content,
                        CombinedScore: result.CombinedScore,
                        VectorScore: result.VectorScore,
                        MetadataScore: result.MetadataScore,
                        EvidenceScore: result.EvidenceScore))
                .ToList();

        return new KnowledgeRetrievalResult(
            Items: rankedResults);
    }
}