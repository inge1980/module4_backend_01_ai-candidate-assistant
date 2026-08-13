using Infrastructure.LLM;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/llm")]
public class LlmController : ControllerBase
{
    private readonly LlmClientFactory _llmClientFactory;

    public LlmController(
        LlmClientFactory llmClientFactory)
    {
        _llmClientFactory = llmClientFactory;
    }

    [HttpGet("test")]
    public async Task<IActionResult> Test(
        CancellationToken cancellationToken)
    {
        var client =
            _llmClientFactory.Create();

        var response =
            await client.GenerateAsync(
                "Say hello in one sentence.",
                cancellationToken);

        return Ok(new
        {
            response
        });
    }
}