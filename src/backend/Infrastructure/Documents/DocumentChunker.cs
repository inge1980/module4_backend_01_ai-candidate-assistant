using System.Text.RegularExpressions;

namespace Infrastructure.Documents;

public class DocumentChunker
{
    public List<DocumentChunk> Chunk(string markdown, string source, Dictionary<string, string>? metadata = null)
    {
        var chunks = new List<DocumentChunk>();
        var sections = SplitIntoSections(markdown);

        for (var index = 0; index < sections.Count; index++)
        {
            var section = sections[index];
            var content = CleanContent(section.Content);

            if (string.IsNullOrWhiteSpace(content))
                continue;

            var chunkId =
                $"{source}-{index:D3}-{section.Title}";

            chunks.Add(new DocumentChunk
            {
                Id = chunkId,
                Source = source,
                Section = section.Title,
                Content = content,
                Metadata = metadata ?? new Dictionary<string, string>()
            });

        }

        return chunks;
    }


    private List<MarkdownSection> SplitIntoSections(string markdown)
    {
        var sections = new List<MarkdownSection>();

        var matches = Regex.Split(
            markdown,
            @"(?=^#{1,3}\s)",
            RegexOptions.Multiline
        );

        foreach (var part in matches)
        {
            if (string.IsNullOrWhiteSpace(part))
                continue;

            var lines = part.Split(
                '\n',
                StringSplitOptions.RemoveEmptyEntries
            );

            var firstLine = lines.FirstOrDefault();

            if (firstLine == null)
                continue;


            var title = firstLine
                .Trim()
                .TrimStart('#')
                .Trim();

            sections.Add(new MarkdownSection
            {
                Title = title,
                Content = part
            });
        }

        return sections;
    }

    private string CleanContent(string content)
    {
        var lines = content
            .Replace("\r\n", "\n")
            .Split('\n')
            .ToList();

        if (lines.Count > 0 && lines[0].StartsWith("#"))
        {
            lines.RemoveAt(0);
        }

        return string.Join("\n", lines)
            .Replace("---", "")
            .Trim();
    }

    private class MarkdownSection
    {
        public string Title { get; set; } = string.Empty;

        public string Content { get; set; } = string.Empty;
    }
}