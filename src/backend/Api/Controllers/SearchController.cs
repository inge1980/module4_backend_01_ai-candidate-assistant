using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/search")]
public class SearchController : ControllerBase
{

[HttpGet]

public IActionResult Search(
string query)
{


return Ok(new
{
message="Search endpoint ready",
query
});


}

}