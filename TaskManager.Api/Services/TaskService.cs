using Microsoft.EntityFrameworkCore;
using TaskManager.Api.DTOs;

namespace TaskManager.Api.Services;

public class TaskService : ITaskService
{
    private readonly AppDbContext _context;

    public TaskService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TaskItem> CreateAsync(TaskCreateDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Description))
            throw new ArgumentException("Description is required.");

        var task = new TaskItem
        {
            Description = dto.Description,
            CreatedAt = DateTime.Now,
            IsCompleted = false
        };

        _context.Tasks.Add(task);
        await _context.SaveChangesAsync();

        return task;
    }

    public async Task<TaskItem> UpdateAsync(int id, TaskUpdateDto dto)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
            throw new KeyNotFoundException("Tarefa não encontrada.");

        if (task.IsCompleted)
            throw new InvalidOperationException("Tarefas completadas não podem ser editadas.");

        if (string.IsNullOrWhiteSpace(dto.Description))
            throw new ArgumentException("Descrição é obrigatória.");

        task.Description = dto.Description;

        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<TaskItem> CompleteAsync(int id)
    {
        var task = await _context.Tasks.FindAsync(id);

        if (task == null)
            throw new KeyNotFoundException("Tarefa não encontrada.");

        if (task.IsCompleted)
            throw new InvalidOperationException("Tarefa já está completada.");

        task.IsCompleted = true;
        task.CompletedAt = DateTime.Now;

        await _context.SaveChangesAsync();
        return task;
    }

    public async Task<List<TaskItem>> GetAsync(bool? completed)
    {
        var query = _context.Tasks.AsQueryable();

        if (completed.HasValue)
        {
            query = query.Where(t => t.IsCompleted == completed.Value);
        }

        return await query
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<TaskItem>> GetAllAsync()
    {
        return await _context.Tasks
            .OrderByDescending(t => t.CreatedAt)
            .ToListAsync();
    }
}
