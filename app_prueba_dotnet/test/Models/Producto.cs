using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models;

[Table("producto")] 
public class Producto
{   
    [Key]
    [Column("id")] 
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    [Column("nombre")] 
    public string? Nombre { get; set; } = String.Empty;

    [Column("precio")] 
    public decimal Precio { get; set; }

    [Column("stock")] 
    public int Stock { get; set; }

    // La columna 'descripcion' no existe en tu tabla de Postgres, 
    // así que la comentamos para que no de error.
    // public string? Descripcion { get; set; } 
}