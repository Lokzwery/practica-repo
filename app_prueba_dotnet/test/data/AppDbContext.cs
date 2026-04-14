
using Microsoft.EntityFrameworkCore;
using test.Models;

namespace test.data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
public DbSet<Producto> Productos => Set<Producto>();
}
