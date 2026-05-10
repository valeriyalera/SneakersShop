using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SneakersShop.Models;

namespace SneakersShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SneakersController : ControllerBase
{
    private readonly AppDbContext _context;

    public SneakersController(AppDbContext context)
    {
        _context = context;
        
        if (!_context.Sneakers.Any())
        {
            _context.Sneakers.AddRange(
                new Sneaker { BrandId = 1, ModelName = "Air Max 90", Price = 120, SizeId = 1, CategoryId = 1 },
                new Sneaker { BrandId = 2, ModelName = "Ultraboost", Price = 180, SizeId = 2, CategoryId = 1 },
                new Sneaker { BrandId = 1, ModelName = "Dunk Low", Price = 110, SizeId = 1, CategoryId = 2 }
            );
            _context.SaveChanges();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var sneakers = await _context.Sneakers.ToListAsync();
        return Ok(sneakers);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var sneaker = await _context.Sneakers.FindAsync(id);
        if (sneaker == null)
            return NotFound();
        return Ok(sneaker);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Sneaker sneaker)
    {
        _context.Sneakers.Add(sneaker);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = sneaker.Id }, sneaker);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Sneaker sneaker)
    {
        if (id != sneaker.Id)
            return BadRequest();
        
        _context.Entry(sneaker).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var sneaker = await _context.Sneakers.FindAsync(id);
        if (sneaker == null)
            return NotFound();
        
        _context.Sneakers.Remove(sneaker);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}