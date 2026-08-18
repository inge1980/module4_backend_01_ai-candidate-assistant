using Application.Questions;
using Application.Knowledge;
using Infrastructure.LLM;

namespace Api.Services;

public sealed class QuestionService(
    IKnowledgeRetrievalService knowledgeRetrievalService,
    LlmClientFactory llmClientFactory)
    : IQuestionService
{
    private const int RetrievalLimit = 10;
    private const int PromptContextLimit = 5;

    public async Task<AskQuestionResponse> AskAsync(
        string question,
        bool includeDebug = false,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(question))
        {
            throw new ArgumentException(
                "Question cannot be empty.",
                nameof(question));
        }

        // --------------------------------------------------------
        // 1. Retrieve and rank knowledge
        // --------------------------------------------------------

        var retrieval =
            await knowledgeRetrievalService.RetrieveAsync(
                query: question,
                retrievalLimit: RetrievalLimit,
                cancellationToken: cancellationToken);

        // --------------------------------------------------------
        // 2. Select the context that will be sent to the LLM
        // --------------------------------------------------------

        var promptResults =
            retrieval.Items
                .Take(PromptContextLimit)
                .ToList();

        // --------------------------------------------------------
        // 3. Build the context for the answer prompt
        // --------------------------------------------------------

        var context =
            string.Join(
                "\n\n",
                promptResults.Select(
                    (result, index) =>
                        $"[{index + 1}] {result.Source}\n" +
                        $"Project: {GetProjectTitle(result)}\n" +
                        $"Heading: {result.Heading}\n" +
                        $"Semantic Type: {result.SemanticType}\n" +
                        $"Content: {result.Content}"));

        // --------------------------------------------------------
        // 4. Load the answer prompt template
        // --------------------------------------------------------

        var promptTemplate =
            await LoadAnswerPromptAsync(
                cancellationToken);

        // --------------------------------------------------------
        // 5. Build the final LLM prompt
        // --------------------------------------------------------

        var prompt =
            promptTemplate
                .Replace("{{question}}", question)
                .Replace("{{context}}", context);

        // --------------------------------------------------------
        // 6. Send prompt to the configured LLM
        // --------------------------------------------------------

        var client =
            llmClientFactory.Create();

        var answer =
            await client.GenerateAsync(
                prompt,
                cancellationToken);

        // --------------------------------------------------------
        // 7. Map retrieval results to API sources
        // --------------------------------------------------------

        var sources =
            promptResults
                .Select(
                    result =>
                        new QuestionSource(
                            ProjectId: GetProjectId(result.Source),
                            Title: GetProjectTitle(result),
                            Url: GetProjectUrl(result.Source),
                            Heading: result.Heading,
                            SemanticType: result.SemanticType,
                            Content: result.Content,
                            Source: includeDebug
                                ? result.Source
                                : null,
                            Relevance: includeDebug
                                ? new QuestionRelevance(
                                    Combined: result.CombinedScore,
                                    Vector: result.VectorScore,
                                    Metadata: result.MetadataScore,
                                    Evidence: result.EvidenceScore)
                                : null))
                .ToList();

        // --------------------------------------------------------
        // 8. Return final API response
        // --------------------------------------------------------

        return new AskQuestionResponse(
            Answer: answer,
            Sources: sources);
    }

    private static async Task<string> LoadAnswerPromptAsync(
        CancellationToken cancellationToken)
    {
        var promptPath =
            Path.Combine(
                AppContext.BaseDirectory,
                "Prompts",
                "answer",
                "answer-prompt-v6.md");

        if (!File.Exists(promptPath))
        {
            throw new FileNotFoundException(
                $"Could not find answer prompt at: {promptPath}");
        }

        return await File.ReadAllTextAsync(
            promptPath,
            cancellationToken);
    }

    private static string GetProjectId(
        string source)
    {
        return Path.GetFileNameWithoutExtension(source);
    }

    private static string GetProjectTitle(
        KnowledgeRetrievalItem result)
    {
        if (result.Metadata.TryGetValue(
                "title",
                out var title)
            && title is string titleString
            && !string.IsNullOrWhiteSpace(titleString))
        {
            return titleString;
        }

        return GetProjectId(result.Source);
    }

    private static string GetProjectUrl(
        string source)
    {
        var projectId =
            GetProjectId(source);

        return $"/projects/{projectId}";
    }
}