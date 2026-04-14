using test.data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// 1. Servicios básicos
builder.Services.AddControllers();

// 2. Configuración de Base de Datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// 3. Soporte nativo para OpenAPI (Reemplaza a SwaggerGen)
builder.Services.AddOpenApi();

var app = builder.Build();

// 4. Configuración del Pipeline de HTTP
if (app.Environment.IsDevelopment())
{
    // Expone el JSON de la API en /openapi/v1.json
    app.MapOpenApi();
}
else
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.MapControllers(); // Asegúrate de tener esto para que tus controladores funcionen

// 5. Aplicar migraciones automáticamente al iniciar
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Esto aplicará las migraciones pendientes en cada inicio
    db.Database.Migrate();
}

app.Run();