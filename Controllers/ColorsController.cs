using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SneakersShop.Models;

namespace SneakersShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ColorsController : ControllerBase
{
    private readonly AppDbContext _context;

    public ColorsController(AppDbContext context)
    {
        _context = context;
        
        if (!_context.Colors.Any())
        {
            _context.Colors.AddRange(
                new Color { Name = "Червоний" },
                new Color { Name = "Синій" },
                new Color { Name = "Чорний" },
                new Color { Name = "Білий" },
                new Color { Name = "Зелений" },
                new Color { Name = "Жовтий" }
            );
            _context.SaveChanges();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var colors = await _context.Colors.ToListAsync();
        return Ok(colors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var color = await _context.Colors.FindAsync(id);
        if (color == null)
            return NotFound();
        return Ok(color);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Color color)
    {
        _context.Colors.Add(color);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = color.Id }, color);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Color color)
    {
        if (id != color.Id)
            return BadRequest();
        
        _context.Entry(color).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var color = await _context.Colors.FindAsync(id);
        if (color == null)
            return NotFound();
        
        _context.Colors.Remove(color);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}