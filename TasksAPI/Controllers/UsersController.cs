using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace TasksAPI.Controllers;

public record UpdateUserRequest(string? Name, int? RoleId, int? DepartmentId);

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

    [HttpGet("data")]
    public async Task<IActionResult> GetUsersData()
    {
        var roles = await Database.Roles.ToListAsync();
        var departments = await Database.Departments.ToListAsync();

        return Ok(new { Roles = roles, Departments = departments });
    }

    [HttpPatch]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest data)
    {
        var user = await Database.Users.FirstOrDefaultAsync();
        if (user == null) return NotFound();
        
        if (data.Name != null) user.Name = data.Name;
        if (data.DepartmentId.HasValue) user.DepartmentId = data.DepartmentId.Value;
        if (data.RoleId.HasValue) user.RoleId = data.RoleId.Value;

        await Database.SaveChangesAsync();
        
        return Ok(user);
    }
}