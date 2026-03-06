using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TasksAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class KanbanController(AppDbContext Database) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetColumns()
    {
        var columns = await Database.KanbanColumns.ToListAsync();

        return Ok(columns);
    }

    [HttpGet("tasks")]
    public async Task<IActionResult> GetTasks()
    {
        var tasks = await Database.Tasks
            .Where(t => t.KanbanColumnId != null)
            .Include(t => t.KanbanColumn)
            .ToListAsync();

        return Ok(tasks);
    }

    [HttpPost("move/{taskId:int}/{columnId:int}")]
    public async Task<IActionResult> MoveTask(int taskId, int columnId)
    {
        var task = await Database.Tasks.FirstOrDefaultAsync(p => p.Id == taskId);
        if (task == null) return NotFound();

        task.KanbanColumnId = columnId;
        await Database.SaveChangesAsync();

        return Ok(task);
    }
}