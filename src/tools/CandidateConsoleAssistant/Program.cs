using Infrastructure.Embeddings;

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

// TEST: Retrieval evaluation code
var questions = new[]
{
    "What experience do I have with .NET and PostgreSQL?",
    "What projects involved React and TypeScript?",
    "What experience do I have with ERP systems?",
    "What experience do I have with GDPR and form builders?",
    "What experience do I have with ASP.NET Core?",
    "Have you worked with Docker?",
    "What Azure experience do you have?",
    "How did you authenticate your Azure deployment?",
    "Have you worked with CI/CD?",
    "Have you built systems involving PostgreSQL?",
    "Have you worked with Terraform?",
    "What experience do I have with Terraform?",
    "What projects demonstrate backend development?",
    "Have you worked with APIs and integrations?"
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

    var embedding =
        await embeddingService.Create(question);

    Console.WriteLine(
        $"Embedding dimensions: {embedding.Length}");

    Console.WriteLine();
    Console.WriteLine("Searching PostgreSQL...");

    var results =
        await vectorStore.SearchAsync(
            embedding,
            limit: 5);

    Console.WriteLine();
    Console.WriteLine(
        $"Results returned: {results.Count}");

    var rank = 1;

    foreach (var result in results)
    {
        Console.WriteLine();
        Console.WriteLine(
            $"#{rank} | Similarity: {result.Similarity:F4}");

        Console.WriteLine(
            $"Source: {result.Chunk.Source}");

        Console.WriteLine(
            $"Section: {result.Chunk.Section}");

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