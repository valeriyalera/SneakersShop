using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SneakersShop.Models;

namespace SneakersShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SizesController : ControllerBase
{
    private readonly AppDbContext _context;

    public SizesController(AppDbContext context)
    {
        _context = context;
        
        if (!_context.Sizes.Any())
        {
            _context.Sizes.AddRange(
                new Size { Name = "36" },
                new Size { Name = "37" },
                new Size { Name = "38" },
                new Size { Name = "39" },
                new Size { Name = "40" },
                new Size { Name = "41" },
                new Size { Name = "42" },
                new Size { Name = "43" },
                new Size { Name = "44" },
                new Size { Name = "45" }
            );
            _context.SaveChanges();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var sizes = await _context.Sizes.ToListAsync();
        return Ok(sizes);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var size = await _context.Sizes.FindAsync(id);
        if (size == null)
            return NotFound();
        return Ok(size);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Size size)
    {
        _context.Sizes.Add(size);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = size.Id }, size);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Size size)
    {
        if (id != size.Id)
            return BadRequest();
        
        _context.Entry(size).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var size = await _context.Sizes.FindAsync(id);
        if (size == null)
            return NotFound();
        
        _context.Sizes.Remove(size);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}