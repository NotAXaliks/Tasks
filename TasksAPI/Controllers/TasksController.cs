using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasksAPI.Models;

namespace TasksAPI.Controllers;

public record UpdateTaskRequest(string? Name, DateTimeOffset? StartDate, DateTimeOffset? EndDate, int? KanbanColumnId);

[ApiController]
[Route("[controller]")]
public class TasksController(AppDbContext Database) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await Database.Tasks.ToListAsync();

        return Ok(tasks);
    }

    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] Tasks data)
    {
        await Database.Tasks.AddAsync(data);

        return Ok(data);
    }
    
    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskRequest data)
    {
        var task = await Database.Tasks.FirstOrDefaultAsync(p => p.Id == id);
        if (task == null) return NotFound();

        if (data.Name != null) task.Name = data.Name;
        if (data.StartDate.HasValue) task.StartDate = data.StartDate.Value;
        if (data.EndDate.HasValue) task.EndDate = data.EndDate.Value;
        if (data.KanbanColumnId.HasValue) task.KanbanColumnId = data.KanbanColumnId;

        return Ok(task);
    }
}