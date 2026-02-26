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
}