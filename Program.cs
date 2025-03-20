using Microsoft.EntityFrameworkCore;
using taskApp.Data;

/// <summary>
/// Clase principal para configurar y ejecutar la aplicaci�n web.
/// </summary>
public class Program
{
    /// <summary>
    /// Punto de entrada principal para la aplicaci�n.
    /// </summary>
    /// <param name="args">Argumentos de l�nea de comandos.</param>
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Configura el contexto de la base de datos en memoria.
        builder.Services.AddDbContext<AppDBContext>(options =>
            options.UseInMemoryDatabase("TareasDB"));

        // Agrega servicios de controladores.
        builder.Services.AddControllers();

        // Agrega servicios para la exploraci�n de endpoints de API.
        builder.Services.AddEndpointsApiExplorer();

        // Agrega servicios para la generaci�n de Swagger.
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

        // Configura Swagger para la documentaci�n de la API.
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "Task API v1");
            c.RoutePrefix = string.Empty;
        });

        // Configura la autorizaci�n.
        app.UseAuthorization();

        // Mapea los controladores.
        app.MapControllers();

        // Asegura que la base de datos est� creada.
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDBContext>();
            context.Database.EnsureCreated();
        }

        // Ejecuta la aplicaci�n.
        app.Run();
    }
}