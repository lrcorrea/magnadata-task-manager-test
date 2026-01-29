using TaskManager.Api.DTOs;

namespace TaskManager.Api.Services;

public interface ITaskService
{
    Task<TaskItem> CreateAsync(TaskCreateDto dto);
    Task<List<TaskItem>> GetAllAsync();

    Task<TaskItem> UpdateAsync(int id, TaskUpdateDto dto);
    Task<TaskItem> CompleteAsync(int id);
    Task<List<TaskItem>> GetAsync(bool? completed);
}