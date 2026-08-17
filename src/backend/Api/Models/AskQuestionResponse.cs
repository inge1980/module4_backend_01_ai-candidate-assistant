namespace Api.Models;

public sealed record AskQuestionResponse(
    string Answer,
    IReadOnlyList<QuestionSource> Sources
);