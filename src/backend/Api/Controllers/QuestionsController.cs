using Api.Models;
using Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("api/v1/[controller]/")]
[ApiController]
public class QuestionsController(IQuestionService service) : ControllerBase
{

    /// <summary>
    /// Henter alle spørsmål.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<QuestionItem>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IEnumerable<QuestionItem>>> Get()
    {
        var questions = await service.GetAllAsync();

        return Ok(questions);
    }


    /// <summary>
    /// Henter ett spørsmål basert på id.
    /// </summary>
    [HttpGet("{id}")]
    [ProducesResponseType(typeof(QuestionItem), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int id)
    {
        var question = await service.GetByIdAsync(id);

        if (question == null)
        {
            return NotFound();
        }

        return Ok(question);
    }


    /// <summary>
    /// Send et nytt spørsmål
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(QuestionItem), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Post(QuestionItem question)
    {
        if (string.IsNullOrWhiteSpace(question.Title))
        {
            return BadRequest(new ProblemDetails
            {
                Title = "Validation error",
                Detail = "Title is required",
                Status = StatusCodes.Status400BadRequest
            });
        }

        var createdQuestion = await service.CreateAsync(question);

        return CreatedAtAction(
            nameof(Get),
            new { id = createdQuestion.Id },
            createdQuestion
        );
    }
}