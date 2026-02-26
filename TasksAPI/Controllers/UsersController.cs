using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TasksAPI.Controllers;

public record UpdateUserRequest(string? Name);

[ApiController]
[Route("[controller]")]
public class UsersController(AppDbContext Database) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetUser()
    {
        var user = await Database.Users
            .Include(p => p.Role)
            .Include(p => p.Department)
            .FirstOrDefaultAsync();
        if (user == null) return NotFound();

        return Ok(user);
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest data)
    {
        var user = await Database.Users.FirstOrDefaultAsync();
        if (user == null) return NotFound();
        
        if (data.Name != null) user.Name = data.Name;

        await Database.SaveChangesAsync();
        
        return Ok(user);
    }
}