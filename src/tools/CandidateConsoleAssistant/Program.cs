using Infrastructure.Embeddings;
using Infrastructure.Reranking;

const int retrievalLimit = 10; // Limit the number of results retrieved from the vector store

var rootDirectory =
    Directory.GetCurrentDirectory();

var envPath =
    Path.Combine(
        rootDirectory,
        ".env");

if (!File.Exists(envPath))
{
    throw new FileNotFoundException(
        $"Could not find .env file at: {envPath}");
}

DotNetEnv.Env.Load(envPath);

Console.WriteLine(
    $"Loaded environment from: {envPath}");

var embeddingService =
    new EmbeddingService();

var vectorStore =
    new VectorStore();

var evidenceScorer =
    new MetadataEvidenceScorer();

// TEST: Retrieval evaluation code
var questions = new[]
{
    // Broad technology experience
    "What experience do I have with ASP.NET Core?",

    // Specific implementation detail
    "How did you authenticate your Azure deployment?",

    // Known ranking problem
    "Have you worked with CI/CD?",

    // Potential false positive / noisy retrieval
    "Have you built systems involving PostgreSQL?",

    // Broader domain experience
    "What experience do I have with ERP systems?" //,

    // Other specific technologies
    /* "What experience do I have with .NET and PostgreSQL?",
    "What projects involved React and TypeScript?",
    "What experience do I have with GDPR and form builders?",
    "Have you worked with Docker?",
    "What Azure experience do you have?",
    "Have you worked with Terraform?",
    "What experience do I have with Terraform?",
    "What projects demonstrate backend development?",
    "Have you worked with APIs and integrations?" */
};

Console.WriteLine();
Console.WriteLine("==============================");
Console.WriteLine("RAG RETRIEVAL EVALUATION");
Console.WriteLine("==============================");

foreach (var question in questions)
{
    Console.WriteLine();
    Console.WriteLine("==============================");
    Console.WriteLine($"Question: {question}");
    Console.WriteLine("==============================");
    Console.WriteLine();
    Console.WriteLine("Generating query embedding...");

    var embedding = await embeddingService.Create(question);
    Console.WriteLine(
        $"Embedding dimensions: {embedding.Length}");
    Console.WriteLine();
    Console.WriteLine("Searching PostgreSQL...");

    var results = await vectorStore.SearchAsync(embedding, limit: retrievalLimit);
    foreach (var result in results) {
        evidenceScorer.Score(question, result);
    }

    results = results
        .OrderByDescending(
                result => result.CombinedScore
        )
        .ToList();

    Console.WriteLine();
    Console.WriteLine(
        $"Results returned: {results.Count}");

    var rank = 1;

    foreach (var result in results)
    {
        Console.WriteLine();
        Console.WriteLine($"#{rank}");
        Console.WriteLine($"Vector score:   {result.VectorScore:F4}");
        Console.WriteLine($"Metadata score: {result.MetadataScore:F4}");
        Console.WriteLine($"Evidence score: {result.EvidenceScore:F4}");
        Console.WriteLine($"Combined score: {result.CombinedScore:F4}");
        Console.WriteLine($"Source:         {result.Chunk.Source}");
        Console.WriteLine($"Section:        {result.Chunk.Section}");

        var preview =
            result.Chunk.Content
                .Replace("\r\n", " ")
                .Replace("\n", " ")
                .Trim();

        if (preview.Length > 300)
        {
            preview =
                preview[..300] + "...";
        }

        Console.WriteLine(
            $"Content: {preview}");

        rank++;
    }
}

Console.WriteLine();
Console.WriteLine("==============================");
Console.WriteLine("RETRIEVAL EVALUATION COMPLETE");
Console.WriteLine("==============================");