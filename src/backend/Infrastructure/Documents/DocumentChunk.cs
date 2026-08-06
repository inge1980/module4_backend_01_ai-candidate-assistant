namespace Infrastructure.Documents;

public class DocumentChunk
{
    public string Id { get; set; } = Guid.NewGuid().ToString();

    public string Source { get; set; } = string.Empty;

    public string Section { get; set; } = string.Empty;

    public string Content { get; set; } = string.Empty;

    public Dictionary<string,string> Metadata { get; set; } = new();
}