namespace Api.Models;

public sealed record QuestionRelevance(
    double Combined,
    double Vector,
    double Metadata,
    double Evidence
);