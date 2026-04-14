using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore; // Necesario para ToListAsync
using test.data;
using test.Models;

namespace test.controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductoController : ControllerBase
{
    private readonly AppDbContext _context;

    public ProductoController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Producto>>> GetProductos()
    {
        // Mejor rendimiento con ToListAsync
        return await _context.Productos.ToListAsync();
    }

    [HttpPost]
public async Task<ActionResult<Producto>> PostProducto([FromBody] Producto producto) // Agregado [FromBody]
{
    if (producto == null)
    {
        return BadRequest("El producto no puede ser nulo.");
    }

    _context.Productos.Add(producto);
    await _context.SaveChangesAsync();
    
    return CreatedAtAction(nameof(GetProductos), new { id = producto.Id }, producto);
}
}