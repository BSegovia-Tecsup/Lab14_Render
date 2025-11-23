using LAB4_Bryan_Segovia.Interfaz;
using LAB4_Bryan_Segovia.Models;
using LAB4_Bryan_Segovia.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (builder.Environment.IsProduction())
{
    var renderConnectionString = Environment.GetEnvironmentVariable("ConnectionStrings__DefaultConnection");
    if (!string.IsNullOrEmpty(renderConnectionString))
    {
        connectionString = renderConnectionString;
    }
}

builder.Services.AddDbContext<DbContextTienda>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "API Tienda BS",
        Version = "v1",
        Description = "API con Swagger",
        Contact = new OpenApiContact
        {
            Name = "Bryan Segovia",
            Email = "bryan.segovia@tecsup.edu.pe"
        }
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // En producción, ajusta la ruta si es necesario.
        var swaggerJsonBasePath = string.IsNullOrWhiteSpace(c.RoutePrefix) ? "." : "..";
        c.SwaggerEndpoint($"{swaggerJsonBasePath}/swagger/v1/swagger.json", "API Tienda BS v1");
        // Sirve Swagger UI en la raíz.
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.MapControllers();
app.Run();