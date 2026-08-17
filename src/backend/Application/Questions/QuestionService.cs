using Application.Questions;

namespace Api.Services;

public sealed class QuestionService : IQuestionService
{
    public async Task<AskQuestionResponse> AskAsync(
        string question,
        bool includeDebug = false,
        CancellationToken cancellationToken = default)
    {
        // TODO:
        // 1. Attach function to enerate embedding for the question
        // 2. Attach function to search the vector database
        // 3. Attach function to retrieve relevant project chunks
        // 4. Attach function to build the LLM prompt
        // 5. Send the prompt to the LLM
        // 6. Map retrieved projects to sources

        await Task.CompletedTask;

        var sources = new List<QuestionSource>
        {
            new(
                ProjectId: "azure-dotnet-devops-demo",
                Title: "Azure .NET DevOps Demo",
                Url: "/projects/azure-dotnet-devops-demo",
                Heading: "Overview",
                SemanticType: "overview",
                Content: "An ASP.NET Core Web API deployed to Microsoft Azure...",
                Source: includeDebug
                    ? "azure-dotnet-devops-demo.md"
                    : null,
                Relevance: includeDebug
                    ? new QuestionRelevance(
                        Combined: 0.4861,
                        Vector: 0.6970,
                        Metadata: 0.1667,
                        Evidence: 0.1750)
                    : null
            )
        };

        return new AskQuestionResponse(
            Answer: "Not implemented yet.",
            Sources: sources
        );

    }
}