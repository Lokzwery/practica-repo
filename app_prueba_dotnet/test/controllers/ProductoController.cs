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

// UPDATE: Actualizar un producto existente (PUT)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutProducto(int id, [FromBody] Producto producto)
    {
        if (id != producto.Id)
        {
            return BadRequest("El ID no coincide con el producto.");
        }

        _context.Entry(producto).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!_context.Productos.Any(e => e.Id == id))
            {
                return NotFound("El producto no existe.");
            }
            else
            {
                throw;
            }
        }

        return NoContent(); // Devuelve un 204 si todo salió bien
    }

    // DELETE: Eliminar un producto (DELETE)
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProducto(int id)
    {
        var producto = await _context.Productos.FindAsync(id);
        if (producto == null)
        {
            return NotFound("Producto no encontrado.");
        }

        _context.Productos.Remove(producto);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}