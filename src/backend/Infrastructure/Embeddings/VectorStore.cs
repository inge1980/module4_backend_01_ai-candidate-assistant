// Embedding to vector

using System.Text.Json;
using Infrastructure.Documents;
using Npgsql;
using Pgvector;

namespace Infrastructure.Embeddings;

public class VectorStore
{
    private readonly NpgsqlDataSource _dataSource;

    public VectorStore()
    {
        var connectionString =
            Environment.GetEnvironmentVariable("POSTGRES_CONNECTION")
            ?? throw new InvalidOperationException(
                "POSTGRES_CONNECTION environment variable is missing.");

        var builder =
            new NpgsqlDataSourceBuilder(connectionString);

        builder.UseVector();

        _dataSource = builder.Build();
    }

    public async Task InsertAsync(
        DocumentChunk chunk,
        float[] embedding)
    {
        if (embedding.Length != 384)
        {
            throw new ArgumentException(
                $"Expected a 384-dimensional embedding, but received {embedding.Length} dimensions.",
                nameof(embedding));
        }

        await using var db =
            await _dataSource.OpenConnectionAsync();

        await using var cmd =
            new NpgsqlCommand(
                """
                INSERT INTO document_chunks
                (
                    id,
                    source,
                    section,
                    content,
                    metadata,
                    embedding
                )
                VALUES
                (
                    @id,
                    @source,
                    @section,
                    @content,
                    @metadata,
                    @embedding
                )
                ON CONFLICT (id)
                DO UPDATE SET
                    source = EXCLUDED.source,
                    section = EXCLUDED.section,
                    content = EXCLUDED.content,
                    metadata = EXCLUDED.metadata,
                    embedding = EXCLUDED.embedding;
                """,
                db);

        cmd.Parameters.AddWithValue(
            "id",
            chunk.Id);

        cmd.Parameters.AddWithValue(
            "source",
            chunk.Source);

        cmd.Parameters.AddWithValue(
            "section",
            chunk.Section);

        cmd.Parameters.AddWithValue(
            "content",
            chunk.Content);

        cmd.Parameters.AddWithValue(
            "metadata",
            NpgsqlTypes.NpgsqlDbType.Jsonb,
            JsonSerializer.Serialize(chunk.Metadata));

        cmd.Parameters.AddWithValue(
            "embedding",
            new Vector(embedding));

        await cmd.ExecuteNonQueryAsync();
    }

    public async Task<IReadOnlyList<DocumentChunk>> SearchAsync(
        float[] embedding,
        int limit = 5)
    {
        if (embedding.Length != 384)
        {
            throw new ArgumentException(
                $"Expected a 384-dimensional embedding, but received {embedding.Length} dimensions.",
                nameof(embedding));
        }

        if (limit <= 0)
        {
            throw new ArgumentOutOfRangeException(
                nameof(limit),
                "Search limit must be greater than zero.");
        }

        await using var db =
            await _dataSource.OpenConnectionAsync();

        await using var cmd =
            new NpgsqlCommand(
                """
                SELECT
                    id,
                    source,
                    section,
                    content,
                    metadata,
                    embedding
                FROM document_chunks
                ORDER BY embedding <=> @embedding
                LIMIT @limit;
                """,
                db);

        cmd.Parameters.AddWithValue(
            "embedding",
            new Vector(embedding));

        cmd.Parameters.AddWithValue(
            "limit",
            limit);

        var results =
            new List<DocumentChunk>();

        await using var reader =
            await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            var metadataJson =
                reader.IsDBNull(4)
                    ? null
                    : reader.GetString(4);

            var metadata =
                string.IsNullOrWhiteSpace(metadataJson)
                    ? new Dictionary<string, string>()
                    : JsonSerializer.Deserialize<Dictionary<string, string>>(
                        metadataJson)
                      ?? new Dictionary<string, string>();

            results.Add(
                new DocumentChunk
                {
                    Id = reader.GetString(0),
                    Source = reader.GetString(1),
                    Section = reader.GetString(2),
                    Content = reader.GetString(3),
                    Metadata = metadata,
                    Embedding = reader.IsDBNull(5)
                        ? default!
                        : reader.GetFieldValue<Vector>(5)
                });
        }

        return results;
    }
}