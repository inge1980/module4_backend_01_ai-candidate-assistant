using Api.Models;

namespace Api.Services;

public interface IQuestionService
{
    Task<IEnumerable<QuestionItem>> GetAllAsync();

    Task<QuestionItem?> GetByIdAsync(int id);

    Task<QuestionItem> CreateAsync(QuestionItem question);
}