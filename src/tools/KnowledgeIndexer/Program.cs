using Infrastructure.Documents;
using Infrastructure.Embeddings;
using System.Text.Json;

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


var knowledgePath =
    Path.Combine(
        rootDirectory,
        "knowledge",
        "projects");

Console.WriteLine(
    $"Loading knowledge from: {knowledgePath}");


var loader =
    new MarkdownDocumentLoader();

var chunker =
    new DocumentChunker();

var documents =
    loader.Load(knowledgePath)
    .ToList();

/* 
// TEST single document and chunking
var document =
    documents.First(
        document =>
            document.FileName ==
            "azure-dotnet-devops-demo.md");

Console.WriteLine(
    $"Document: {document.FileName}");

Console.WriteLine();
Console.WriteLine("Metadata:");

Console.WriteLine(
    JsonSerializer.Serialize(
        document.Metadata,
        new JsonSerializerOptions
        {
            WriteIndented = true
        }));


var chunker =
    new DocumentChunker();

var chunks =
    chunker.Chunk(
        document.Content,
        document.FileName,
        document.Metadata);

var firstChunk =
    chunks.First();

Console.WriteLine();
Console.WriteLine(
    $"Chunk: {firstChunk.Section}");

Console.WriteLine();
Console.WriteLine("Chunk metadata:");

Console.WriteLine(
    JsonSerializer.Serialize(
        firstChunk.Metadata,
        new JsonSerializerOptions
        {
            WriteIndented = true
        }));

*/


// indexing code
Console.WriteLine();
Console.WriteLine("==============================");


var allChunks =
    new List<DocumentChunk>();

// foreach (var document in documents.Take(3))
foreach (var document in documents)
{
    //Console.WriteLine();
    //Console.WriteLine("==============================");
    Console.WriteLine($"Processing filename: {document.FileName}");
    Console.WriteLine(
        $"Metadata fields: {document.Metadata.Count}");

    Console.WriteLine(JsonSerializer.Serialize(
        document.Metadata,
        new JsonSerializerOptions {
            WriteIndented = true
        }
    ));

    //Console.WriteLine($"Title: {document.Metadata["title"]}");

    var chunks =
        chunker.Chunk(
            document.Content,
            document.FileName,
            document.Metadata);

    Console.WriteLine($"  Chunks produced: {chunks.Count}");

    // Filter out chunks with empty content
    var validChunks =
        chunks
            .Where(chunk =>
                !string.IsNullOrWhiteSpace(chunk.Content))
            .ToList();

    allChunks.AddRange(validChunks);

    // foreach (var chunk in chunks.Take(3))
    foreach (var chunk in validChunks)
    {
        //Console.WriteLine();
        //Console.WriteLine($"Section: {chunk.Section}");
        //Console.WriteLine();

        //Console.WriteLine(
        //    chunk.Content[
        //        ..Math.Min(
        //            200,
        //            chunk.Content.Length)]);

        //Console.WriteLine();
        //Console.WriteLine("Generating embedding...");

        var embedding =
            await embeddingService.Create(
                chunk.Content);

        //Console.WriteLine($"Embedding dimensions: {embedding.Length}");

        await vectorStore.InsertAsync(
            chunk,
            embedding);

        //Console.WriteLine("Stored chunk in PostgreSQL.");
    }
}

Console.WriteLine();
Console.WriteLine("==============================");
Console.WriteLine("INDEX SUMMARY");
Console.WriteLine("==============================");

Console.WriteLine(
    $"Documents loaded: {documents.Count()}");

Console.WriteLine(
    $"Documents indexed: {allChunks
        .Select(x => x.Source)
        .Distinct()
        .Count()}");

Console.WriteLine(
    $"Total chunks: {allChunks.Count}");

if (allChunks.Any())
{
    var lengths =
        allChunks
            .Select(x => x.Content.Length)
            .ToList();

    Console.WriteLine();

    Console.WriteLine(
        $"Average chunk length: {lengths.Average():F0}");

    Console.WriteLine(
        $"Shortest chunk: {lengths.Min()}");

    Console.WriteLine(
        $"Longest chunk: {lengths.Max()}");
}