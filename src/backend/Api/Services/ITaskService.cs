using aspnetintro.Model;

namespace aspnetintro.Services;

public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetAllAsync();

    Task<TaskItem?> GetByIdAsync(int id);

    Task<TaskItem> CreateAsync(TaskItem task);
}