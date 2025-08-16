using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Task.Entities;
using TaskManagementSystem.SqlLite;

namespace TaskManagementSystem.Controllers
{

    /// <summary>
    ///  This controller manages task-related operations.
    /// including creation, retrieval, updating, and deletion.
    /// </summary>
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CRUDTaskController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CRUDTaskController(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates a new task.
        /// </summary>
        /// <param name="task">A <see cref="TaskItem"/> object containing all necessary details for the new task.</param>
        /// <returns>Returns the created task with a response code.</returns>
        /// <response code="201">Returns the newly created task.</response>
        /// <response code="400">If the task is invalid.</response>
        [HttpPost("CreateTask",Name = "CreateTask")]
        public IActionResult CreateTask([FromBody] TaskItem task)
        {
            _context.Tasks.Add(task);
            _context.SaveChanges();
            return Ok(task);
        }

        /// <summary>
        /// Retrieves a task details.
        /// </summary>
        /// <returns>An object containing task summary details with response code.</returns>
        /// <response code="200">Returns the list of tasks.</response>
        [HttpGet("GetAllTask", Name = "GetAllTask")]
        public IActionResult GetAllTasks()
        {
            var tasks = _context.Tasks.ToList();
            return Ok(tasks);
        }

        /// <summary>
        /// Updates an existing task (full update).
        /// </summary>
        /// <param name="id">ID of the task.</param>
        /// <param name="task">Updated TaskItem object.</param>
        /// <response code="204">Task updated successfully.</response>
        /// <response code="400">If the task is invalid.</response>
        /// <response code="404">If the task is not found.</response>
        [HttpPut("{id}", Name = "UpdateTask")]
        public IActionResult UpdateTask(int id,[FromBody] TaskItem task)
        {
            var existingTask = _context.Tasks.Find(id);
            if (existingTask == null)
                return NotFound();

            if (task == null)
                return BadRequest("Task is invalid.");
            existingTask.TaskName = task.TaskName;
            existingTask.Status = task.Status;
            existingTask.Estimation = task.Estimation;
            _context.SaveChanges();
            return Ok(existingTask);
        }

        /// <summary>
        /// Partially updates an existing task.
        /// </summary>
        /// <param name="id">ID of the task.</param>
        /// <param name="isCompleted">Completion status to update.</param>
        /// <response code="204">Task updated successfully.</response>
        /// <response code="404">If the task is not found.</response>
        [HttpPatch("{id}",Name = "PartialUpdateTask")]
        public IActionResult PartialUpdateTask(int id, [FromQuery] string Status)
        {
            var existingTask = _context.Tasks.Find(id);
            if(existingTask == null)
            {
                return NotFound();
            }
            if (string.IsNullOrEmpty(Status))
            {
                return BadRequest("Status is Invalid");
            }
            existingTask.Status = Status;
            return NoContent();
        }

        /// <summary>
        /// Gets a task by ID.
        /// </summary>
        /// <param name="id">ID of the task.</param>
        /// <response code="200">Returns the requested task.</response>
        /// <response code="404">If the task is not found.</response>
        [HttpGet("{id}",Name = "GetTaskById")]
        public IActionResult GetTaskById([Required] int id)
        {
            var existingTask = _context.Tasks.Find(id);
            if (existingTask == null) { return NotFound(); }
            return Ok(existingTask);
        }

        /// <summary>
        /// Deletes a task by ID.
        /// </summary>
        /// <param name="id">ID of the task.</param>
        /// <response code="204">Task deleted successfully.</response>
        /// <response code="404">If the task is not found.</response>
        [HttpDelete("{id}", Name = "DeleteTask")]
        public IActionResult DeleteTask(int id)
        {
            var existingTask = _context.Tasks.Find(id);
            if (existingTask == null)
                return NotFound();

           _context.Tasks.Remove(existingTask);
            _context.SaveChanges();
            return NoContent();
        }
    }


}
