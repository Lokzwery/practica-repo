using test.data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Servicios básicos
builder.Services.AddControllers();

// 2. Configuración de Base de Datos 
// --- DESCOMENTA SOLO UNA DE LAS SIGUIENTES OPCIONES ---

// OPCIÓN PARA POSTGRESQL (Activa ahora)
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// OPCIÓN PARA SQL SERVER (Descomenta esta y comenta la de arriba para volver a SQL Server)
/*
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);
*/

// 3. Soporte nativo para OpenAPI
builder.Services.AddOpenApi();

var app = builder.Build();

// 4. Configuración del Pipeline de HTTP
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.MapControllers(); 

// 5. Aplicar migraciones automáticamente
// --- COMENTADO PARA EVITAR ERRORES EN POSTGRES CON DATOS EXISTENTES ---
/*
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Si necesitas usar migraciones en una base limpia, descomenta la línea de abajo
    // db.Database.Migrate();
}
*/

app.Run();