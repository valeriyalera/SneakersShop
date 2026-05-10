using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SneakersShop.Models;

namespace SneakersShop.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BrandsController : ControllerBase
{
    private readonly AppDbContext _context;

    public BrandsController(AppDbContext context)
    {
        _context = context;

        if (!_context.Brands.Any())
        {
            _context.Brands.AddRange(
                new Brand { Name = "Nike" },
                new Brand { Name = "Adidas" },
                new Brand { Name = "New Balance" },
                new Brand { Name = "Puma" }
            );
            _context.SaveChanges();
        }
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var brands = await _context.Brands.ToListAsync();
        return Ok(brands);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var brand = await _context.Brands.FindAsync(id);
        if (brand == null)
            return NotFound();
        return Ok(brand);
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Brand brand)
    {
        _context.Brands.Add(brand);
        await _context.SaveChangesAsync();
        return CreatedAtAction(nameof(Get), new { id = brand.Id }, brand);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] Brand brand)
    {
        if (id != brand.Id)
            return BadRequest();
        
        _context.Entry(brand).State = EntityState.Modified;
        await _context.SaveChangesAsync();
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var brand = await _context.Brands.FindAsync(id);
        if (brand == null)
            return NotFound();
        
        _context.Brands.Remove(brand);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}