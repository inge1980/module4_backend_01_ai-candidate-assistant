namespace Infrastructure.Documents;

public class FrontmatterParser
{
    public ParsedMarkdown Parse(string markdown)
    {
        var result = new ParsedMarkdown();

        if (!markdown.StartsWith("---"))
        {
            result.Content = markdown;
            return result;
        }


        var endIndex = markdown.IndexOf(
            "---",
            3,
            StringComparison.Ordinal
        );


        if (endIndex == -1)
        {
            result.Content = markdown;
            return result;
        }


        var frontmatter = markdown
            .Substring(3, endIndex - 3)
            .Trim();


        foreach (var line in frontmatter.Split(
                     '\n',
                     StringSplitOptions.RemoveEmptyEntries))
        {
            var separator = line.IndexOf(':');

            if (separator == -1)
                continue;


            var key = line[..separator].Trim();

            var value = line[(separator + 1)..].Trim();

            result.Metadata[key] = value;
        }


        result.Content = markdown[(endIndex + 3)..].Trim();

        return result;
    }
}