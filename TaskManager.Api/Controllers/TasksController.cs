using Microsoft.AspNetCore.Mvc;
using TaskManager.Api.DTOs;
using TaskManager.Api.Services;

namespace TaskManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    // [HttpGet]
    // public async Task<IActionResult> Get()
    // {
    //     var tasks = await _taskService.GetAllAsync();
    //     return Ok(tasks);
    // }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> Get([FromQuery] bool? completed)
    {
        var tasks = await _taskService.GetAsync(completed);
        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> Post(TaskCreateDto dto)
    {
        try
        {
            var task = await _taskService.CreateAsync(dto);
            return CreatedAtAction(nameof(Get), new { id = task.Id }, task);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, TaskUpdateDto dto)
    {
        try
        {
            var task = await _taskService.UpdateAsync(id, dto);
            return Ok(task);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPatch("{id}/complete")]
    public async Task<IActionResult> Complete(int id)
    {
        try
        {
            var task = await _taskService.CompleteAsync(id);
            return Ok(task);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }
}