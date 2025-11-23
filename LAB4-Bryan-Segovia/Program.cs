using LAB4_Bryan_Segovia.Interfaz;
using LAB4_Bryan_Segovia.Models;
using LAB4_Bryan_Segovia.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// --- INICIO DE LA CORRECCIÓN FINAL ---
string connectionString;
if (builder.Environment.IsProduction())
{
    // En Producción (Render), OBLIGAMOS a usar la variable de entorno.
    connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
    if (string.IsNullOrEmpty(connectionString))
    {
        throw new InvalidOperationException("DATABASE_URL environment variable not found in Production.");
    }
}
else
{
    // En Desarrollo (tu PC), usamos el appsettings.json.
    connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
}
// --- FIN DE LA CORRECCIÓN FINAL ---

builder.Services.AddDbContext<DbContextTienda>(options =>
    options.UseNpgsql(connectionString));

// ... (el resto del archivo sigue igual)
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Tienda BS", Version = "v1" });
});

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "API Tienda BS v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();