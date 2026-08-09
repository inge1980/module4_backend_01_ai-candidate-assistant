using Infrastructure.Documents;
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
    loader.Load(knowledgePath);

var allChunks =
    new List<DocumentChunk>();

foreach (var document in documents.Take(3))
{
    Console.WriteLine();
    Console.WriteLine("==============================");
    Console.WriteLine(
        $"Filename: {document.FileName}");

    Console.WriteLine(
        $"Title: {document.Metadata["title"]}");

    var chunks =
        chunker.Chunk(
            document.Content,
            document.FileName,
            document.Metadata);

    allChunks.AddRange(chunks);

    foreach (var chunk in chunks.Take(3))
    {
        Console.WriteLine();
        Console.WriteLine(
            $"Section: {chunk.Section}");

        Console.WriteLine();

        Console.WriteLine(
            chunk.Content[
                ..Math.Min(
                    200,
                    chunk.Content.Length)]);

        Console.WriteLine();
        Console.WriteLine("Generating embedding...");

        var embedding =
            await embeddingService.Create(
                chunk.Content);

        Console.WriteLine(
            $"Embedding dimensions: {embedding.Length}");

        await vectorStore.InsertAsync(
            chunk,
            embedding);

        Console.WriteLine(
            "Stored chunk in PostgreSQL.");
    }
}

Console.WriteLine();
Console.WriteLine("==============================");
Console.WriteLine("INDEX SUMMARY");
Console.WriteLine("==============================");

Console.WriteLine(
    $"Total documents loaded: {documents.Count()}");

Console.WriteLine(
    $"Documents with chunks: {allChunks
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