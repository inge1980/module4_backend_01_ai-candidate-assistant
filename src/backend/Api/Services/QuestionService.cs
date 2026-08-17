using Api.Models;

namespace Api.Services;

public class QuestionService : IQuestionService
{
    private readonly List<QuestionItem> questions = new()
    {
        new QuestionItem
        {
            Id = 1,
            Title = "Fix up the boat",
            Description = "Fix all stuff that broke during the last vacation",
            Status = QuestionItemStatus.Open,
            CreatedAt = DateTime.Now,
            DueDate = DateTime.Now.AddDays(7)
        },
        new QuestionItem
        {
            Id = 2,
            Title = "Take a boating trip",
            Description = "Vacation!",
            Status = QuestionItemStatus.Completed,
            CreatedAt = DateTime.Now.AddDays(-2),
            DueDate = null
        }
    };


    public Task<IEnumerable<QuestionItem>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<QuestionItem>>(questions);
    }


    public Task<QuestionItem?> GetByIdAsync(int id)
    {
        var question = questions.FirstOrDefault(q => q.Id == id);

        return Task.FromResult(question);
    }


    public Task<QuestionItem> CreateAsync(QuestionItem question)
    {
        question.Id = questions.Count > 0
            ? questions.Max(q => q.Id) + 1
            : 1;

        question.CreatedAt = DateTime.Now;

        questions.Add(question);

        return Task.FromResult(question);
    }
}