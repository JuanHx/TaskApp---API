using Microsoft.EntityFrameworkCore;
using taskApp.Data;

/// <summary>
/// Clase principal para configurar y ejecutar la aplicación web.
/// </summary>
public class Program
{
    /// <summary>
    /// Punto de entrada principal para la aplicación.
    /// </summary>
    /// <param name="args">Argumentos de línea de comandos.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configura el contexto de la base de datos en memoria.
        builder.Services.AddDbContext<AppDBContext>(options =>
            options.UseInMemoryDatabase("TareasDB"));

        // Agrega servicios de controladores.
        builder.Services.AddControllers();

        // Agrega servicios para la exploración de endpoints de API.
        builder.Services.AddEndpointsApiExplorer();

        // Agrega servicios para la generación de Swagger.
        builder.Services.AddSwaggerGen();

        var corsPolicyName = "AllowGithubPages";
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(corsPolicyName, policy =>
            {
                policy.WithOrigins("https://juanhx.github.io/") // Reemplaza con tu URL real
                      .AllowAnyMethod()
                      .AllowAnyHeader();
            });
        });

        var app = builder.Build();

        // Configura Swagger para la documentación de la API.
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task API v1");
            c.RoutePrefix = string.Empty;
        });

        // Configura la autorización.
        app.UseAuthorization();

        // Mapea los controladores.
        app.MapControllers();

        // Asegura que la base de datos esté creada.
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDBContext>();
            context.Database.EnsureCreated();
        }

        // Ejecuta la aplicación.
        app.Run();
    }
}