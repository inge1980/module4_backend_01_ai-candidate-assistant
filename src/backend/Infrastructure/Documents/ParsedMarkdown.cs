namespace Infrastructure.Documents;

public class ParsedMarkdown
{
    public string Content { get; set; } = "";

    public Dictionary<string,string> Metadata { get; set; } = new();
}