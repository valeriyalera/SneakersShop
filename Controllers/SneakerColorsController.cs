using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SneakersShop.Models;

namespace SneakersShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SneakerColorsController : ControllerBase
{
    private readonly AppDbContext _context;

    public SneakerColorsController(AppDbContext context)
    {
        _context = context;
        
        if (!_context.SneakerColors.Any() && _context.Sneakers.Any() && _context.Colors.Any())
        {
            _context.SneakerColors.AddRange(
                new SneakerColor { SneakerId = 1, ColorId = 1 },
                new SneakerColor { SneakerId = 1, ColorId = 2 },
                new SneakerColor { SneakerId = 2, ColorId = 3 },
                new SneakerColor { SneakerId = 3, ColorId = 4 }
            );
            _context.SaveChanges();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var sneakerColors = await _context.SneakerColors
            .Include(sc => sc.Sneaker)
            .Include(sc => sc.Color)
            .ToListAsync();
        return Ok(sneakerColors);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var sneakerColor = await _context.SneakerColors
            .Include(sc => sc.Sneaker)
            .Include(sc => sc.Color)
            .FirstOrDefaultAsync(sc => sc.Id == id);
        
        if (sneakerColor == null)
            return NotFound();
        return Ok(sneakerColor);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] SneakerColor sneakerColor)
    {
        _context.SneakerColors.Add(sneakerColor);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = sneakerColor.Id }, sneakerColor);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] SneakerColor sneakerColor)
    {
        if (id != sneakerColor.Id)
            return BadRequest();
        
        _context.Entry(sneakerColor).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sneakerColor = await _context.SneakerColors.FindAsync(id);
        if (sneakerColor == null)
            return NotFound();
        
        _context.SneakerColors.Remove(sneakerColor);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}