using aspnetintro.Model;
using aspnetintro.Services;
using Microsoft.AspNetCore.Mvc;

namespace aspnetintro.Controllers;

[Route("api/[controller]/")]
[ApiController]
public class TasksController(ITaskService service) : ControllerBase
{

    /// <summary>
    /// Henter alle oppgaver.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<TaskItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<TaskItem>>> Get()
    {
        var tasks = await service.GetAllAsync();

        return Ok(tasks);
    }


    /// <summary>
    /// Henter én oppgave basert på id.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TaskItem), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var task = await service.GetByIdAsync(id);

        if (task == null)
        {
            return NotFound();
        }

        return Ok(task);
    }


    /// <summary>
    /// Oppretter en ny oppgave.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(TaskItem), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(TaskItem task)
    {
        if (string.IsNullOrWhiteSpace(task.Title))
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Validation error",
                Detail = "Title is required",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var createdTask = await service.CreateAsync(task);

        return CreatedAtAction(
            nameof(Get),
            new { id = createdTask.Id },
            createdTask
        );
    }
}