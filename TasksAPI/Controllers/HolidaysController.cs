using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TasksAPI.Models;

namespace TasksAPI.Controllers;

public record UpdateHolidayRequest(string? Name, DateTimeOffset? StartDate, DateTimeOffset? EndDate);

[ApiController]
[Route("[controller]")]
public class HolidaysController(AppDbContext Database) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetHolidays()
    {
        var holidays = await Database.Holidays.ToListAsync();

        return Ok(holidays);
    }
    
    [HttpPost]
    public async Task<IActionResult> CreateHoliday([FromBody] Holidays data)
    {
        await Database.Holidays.AddAsync(data);
        
        return Ok(data);
    }

    [HttpPatch("{id:int}")]
    public async Task<IActionResult> UpdateHoliday(int id, [FromBody] UpdateHolidayRequest data)
    {
        var holiday = await Database.Holidays.FirstOrDefaultAsync(p => p.Id == id);
        if (holiday == null) return NotFound();

        if (data.Name != null) holiday.Name = data.Name;
        if (data.StartDate.HasValue) holiday.StartDate = data.StartDate.Value;
        if (data.EndDate.HasValue) holiday.EndDate = data.EndDate.Value;

        return Ok(holiday);
    }
}