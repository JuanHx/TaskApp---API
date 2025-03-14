using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using taskApp.Data;
using taskApp.Models;

namespace taskApp.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly AppDBContext _context;

        public TaskController(AppDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Obtiene todas las tareas registradas
        /// </summary>
        /// <returns>Lista de tareas</returns>
        [HttpGet]
        public async Task<List<Tareas>> Get()
        {
            return await _context.Tasks.ToListAsync();
        }

        /// <summary>
        /// Obtiene una tarea por ID
        /// </summary>
        /// <param name="id">ID de la tarea</param>
        /// <returns>Objeto de la tarea</returns>
        [HttpGet("{id:int}", Name = "FindById")]
        public async Task<ActionResult<Tareas>> Get(int id)
        {
            var task = await _context.Tasks.FirstOrDefaultAsync(x => x.id == id);
            if(task == null)
            {
                return NotFound();
            }

            return task;
        }

        /// <summary>
        /// Crea una nueva tarea
        /// </summary>
        /// <param name="task">Objeto de la tarea a crear</param>
        /// <returns>La tarea creada</returns>
        [HttpPost]
        public async Task<ActionResult<Tareas>> Post([FromBody] Tareas task)
        {
            if (task == null || string.IsNullOrWhiteSpace(task.titulo) || string.IsNullOrWhiteSpace(task.descripcion))
            {
                return BadRequest("Titulo y Descripcion son obligatorios.");
            }

            // Inicializa fechas si están vacías
            task.fecha_creacion = task.fecha_creacion == default ? DateTime.Now : task.fecha_creacion;
            task.fecha_actualizacion = task.fecha_actualizacion == default ? DateTime.Now : task.fecha_actualizacion;

            _context.Add(task);
            await _context.SaveChangesAsync();
            return CreatedAtRoute("FindById", new { id = task.id }, task);
        }

        /// <summary>
        /// Actualiza una tarea mediante el ID
        /// </summary>
        /// <param name="id">ID de la tarea</param>
        /// <returns>No Content si se eliminó correctamente</returns>
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] Tareas tarea)
        {
            if (tarea == null || string.IsNullOrWhiteSpace(tarea.titulo) || string.IsNullOrWhiteSpace(tarea.descripcion))
            {
                return BadRequest("Titulo y Descripcion son obligatorios.");
            }

            var existingTask = await _context.Tasks.FindAsync(id);
            if (existingTask == null)
            {
                return NotFound();
            }

            existingTask.titulo = tarea.titulo;
            existingTask.descripcion = tarea.descripcion;
            existingTask.estado = tarea.estado;
            existingTask.fecha_actualizacion = DateTime.Now;

            await _context.SaveChangesAsync();
            return NoContent();
        }

        /// <summary>
        /// Elimina una tarea por ID
        /// </summary>
        /// <param name="id">ID de la tarea</param>
        /// <returns>No Content si se eliminó correctamente</returns>
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var task = await _context.Tasks.FindAsync(id);

            if (task == null)
            {
                return NotFound();
            }

            _context.Tasks.Remove(task);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        /// <summary>
        /// Filtra las tareas segun el estado enviado
        /// </summary>
        /// <param name="estado">Estado de la tarea</param>
        /// <returns>Lista de tareas encontradas</returns>
        [HttpGet("filtrar")]
        public async Task<ActionResult<IEnumerable<Tareas>>> GetByEstado([FromQuery] Tareas.Estado estado)
        {
            var tareas = await _context.Tasks.Where(t => t.estado == estado).ToListAsync();

            if (tareas == null)
            {
                return NotFound("No hay tareas con este estado.");
            }

            return Ok(tareas);
        }
    }
}
