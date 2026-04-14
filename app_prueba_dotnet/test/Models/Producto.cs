using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test.Models;

public class Producto
{
    public int Id{get; set;}
    [Required]
    [MaxLength(100)]
public string? Nombre {get; set;} = String.Empty;
[MaxLength(300)]
public String? Descripcion {get; set;}
[Column(TypeName ="decimal(18,2)")]
public decimal Precio{get;set;}
public int Stock {get;set;}
}
