using Infrastructure.Embeddings;
using Infrastructure.Reranking;

const int retrievalLimit = 10; // Limit the number of results retrieved from the vector store
const int promptContextLimit = 5;

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

var promptPath =
    Path.Combine(
        rootDirectory,
        "prompts",
        "answer",
        "answer-prompt-v6.md");

if (!File.Exists(promptPath))
{
    throw new FileNotFoundException(
        $"Could not find answer prompt at: {promptPath}");
}

var answerPromptTemplate =
    await File.ReadAllTextAsync(promptPath);

var embeddingService =
    new EmbeddingService();

var vectorStore =
    new VectorStore();

var evidenceScorer =
    new MetadataEvidenceScorer();

// TEST: Retrieval evaluation code
var questions = new[]
{
    // Test small variations of the same question to evaluate retrieval and ranking
    "Have you used PostgreSQL?",
    "Have you used PostgreSQL in production?",
    "Have you used PostgreSQL in a school project?",
    "Have you used PostgreSQL for personal projects?",
    "What production experience do I have?"

    // PostgreSQL-related questions to evaluate retrieval and ranking
    //"Have you built systems involving PostgreSQL?",     //  Multiple projects 
    //"What experience do I have with PostgreSQL?",       // Broad knowledge and specific examples
    //"Have you used pgvector?",                          // RAG-prosject ranked as nr 1, but also other projects
    //"Have you used PostgreSQL with .NET?",              // Lost & Found high ranked
    //"What databases have I worked with?",               // PostgreSQL + others
    //"Have you used PostgreSQL in production?"           // Should avoid school projects and focus on real-world experience

/* 
// Initial test questions for retrieval evaluation
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
 */

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
    // Console.WriteLine("Generating query embedding...");

    var embedding = await embeddingService.Create(question);
    // Console.WriteLine($"Embedding dimensions: {embedding.Length}");
    // Console.WriteLine();
    // Console.WriteLine("Searching PostgreSQL...");

    var results = await vectorStore.SearchAsync(embedding, limit: retrievalLimit);
    foreach (var result in results) {
        evidenceScorer.Score(question, result);
    }

    results = results
        .OrderByDescending(
                result => result.CombinedScore
        )
        .ToList();

    // Console.WriteLine();
    // Console.WriteLine($"Results returned: {results.Count}");

    var rank = 1;

    foreach (var result in results)
    {
        Console.WriteLine();
        Console.WriteLine($"#{rank} Combined: {result.CombinedScore:F4}");
        Console.WriteLine($"   Vector: {result.VectorScore:F4}, Metadata: {result.MetadataScore:F4}, Evidence: {result.EvidenceScore:F4}");
        Console.WriteLine($"   Source: {result.Chunk.Source}");
        Console.WriteLine($"   Heading: {result.Chunk.HeadingPath}");
        Console.WriteLine($"   Semantic Type: {result.Chunk.SemanticType}");

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

        Console.WriteLine($"   Content: {preview}");

        rank++;
    }

    var context =
        string.Join(
            "\n\n",
            results
                .Take(promptContextLimit)
                .Select(
                    (result, index) =>
                        $"[{index + 1}] {result.Chunk.Source}\n" +
                        $"Heading: {result.Chunk.HeadingPath}\n" +
                        $"Semantic Type: {result.Chunk.SemanticType}\n" +
                        $"Content: {result.Chunk.Content}"));

    var prompt =
        answerPromptTemplate
            .Replace("{{question}}", question)
            .Replace("{{context}}", context);

    Console.WriteLine();
    Console.WriteLine("==============================");
    Console.WriteLine("GENERATED ANSWER PROMPT");
    Console.WriteLine("==============================");
    Console.WriteLine();
    Console.WriteLine(prompt);
    Console.WriteLine();
    Console.WriteLine("==============================");
    Console.WriteLine("GENERATED ANSWER PROMPT COMPLETE");
    Console.WriteLine("==============================");
}

Console.WriteLine();
Console.WriteLine("==============================");
Console.WriteLine("RETRIEVAL EVALUATION COMPLETE");
Console.WriteLine("==============================");