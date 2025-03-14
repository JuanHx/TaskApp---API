using Microsoft.EntityFrameworkCore;
using taskApp.Models;

namespace taskApp.Data
{
    /// <summary>
    /// Contexto de base de datos para la aplicación de tareas.
    /// </summary>
    public class AppDBContext : DbContext
    {

        /// <summary>
        /// Constructor que acepta opciones de configuración para el contexto.
        /// </summary>
        /// <param name="options">Opciones de configuración para el contexto.</param>
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options) { }

        /// <summary>
        /// Conjunto de datos que representa las tareas.
        /// </summary>
        public DbSet<Tareas> Tasks { get; set; }

        /// <summary>
        /// Configura el modelo de datos mediante la API Fluent.
        /// </summary>
        /// <param name="modelBuilder">Constructor del modelo.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tareas>(tb =>
            {
                tb.HasKey(col => col.id);
                tb.Property(col => col.id).ValueGeneratedOnAdd();
                tb.Property(col => col.titulo).IsRequired();
            });

            modelBuilder.Entity<Tareas>().HasData(
                new Tareas
                {
                    id = 1,
                    titulo = "Tarea 1",
                    descripcion = "Descripcion de la tarea 1",
                    estado = Tareas.Estado.Pendiente,
                    fecha_creacion = System.DateTime.Now,
                    fecha_actualizacion = System.DateTime.Now
                },
                new Tareas
                {
                    id = 2,
                    titulo = "Tarea 2",
                    descripcion = "Descripcion de la tarea 2",
                    estado = Tareas.Estado.EnProgreso,
                    fecha_creacion = System.DateTime.Now,
                    fecha_actualizacion = System.DateTime.Now
                },
                new Tareas
                {
                    id = 3,
                    titulo = "Tarea 3",
                    descripcion = "Descripcion de la tarea 3",
                    estado = Tareas.Estado.Completado,
                    fecha_creacion = System.DateTime.Now,
                    fecha_actualizacion = System.DateTime.Now
                }
            );
        }
    }
}
