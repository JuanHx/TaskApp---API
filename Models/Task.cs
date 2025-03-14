namespace taskApp.Models
{
    /// <summary>
    /// Representa una tarea en la aplicación.
    /// </summary>
    public class Tareas
    {
        /// <summary>
        /// Identificador único de la tarea.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// Título de la tarea.
        /// </summary>
        public string titulo { get; set; }

        /// <summary>
        /// Descripción de la tarea.
        /// </summary>
        public string descripcion { get; set; }

        /// <summary>
        /// Estado actual de la tarea.
        /// </summary>
        public Estado estado { get; set; }

        /// <summary>
        /// Fecha de creación de la tarea.
        /// </summary>
        public DateTime fecha_creacion { get; set; }

        /// <summary>
        /// Fecha de la última actualización de la tarea.
        /// </summary>
        public DateTime fecha_actualizacion { get; set; }

        /// <summary>
        /// Enum que representa los posibles estados de una tarea.
        /// </summary>
        public enum Estado
        {
            Pendiente,
            EnProgreso,
            Completado
        }
    }
}

