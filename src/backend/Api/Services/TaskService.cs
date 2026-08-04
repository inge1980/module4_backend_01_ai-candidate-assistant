using aspnetintro.Model;

namespace aspnetintro.Services;

public class TaskService : ITaskService
{
    private readonly List<TaskItem> tasks = new()
    {
        new TaskItem
        {
            Id = 1,
            Title = "Fix up the boat",
            Description = "Fix all stuff that broke during the last vacation",
            Status = TaskItemStatus.Open,
            CreatedAt = DateTime.Now,
            DueDate = DateTime.Now.AddDays(7)
        },
        new TaskItem
        {
            Id = 2,
            Title = "Take a boating trip",
            Description = "Vacation!",
            Status = TaskItemStatus.Completed,
            CreatedAt = DateTime.Now.AddDays(-2),
            DueDate = null
        }
    };


    public Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return Task.FromResult<IEnumerable<TaskItem>>(tasks);
    }


    public Task<TaskItem?> GetByIdAsync(int id)
    {
        var task = tasks.FirstOrDefault(t => t.Id == id);

        return Task.FromResult(task);
    }


    public Task<TaskItem> CreateAsync(TaskItem task)
    {
        task.Id = tasks.Count > 0
            ? tasks.Max(t => t.Id) + 1
            : 1;

        task.CreatedAt = DateTime.Now;

        tasks.Add(task);

        return Task.FromResult(task);
    }
}