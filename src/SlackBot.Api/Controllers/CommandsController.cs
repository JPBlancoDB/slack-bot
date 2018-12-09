using Microsoft.AspNetCore.Mvc;

namespace SlackBot.Api.Controllers
{
    [Route("api/[controller]")]
    public class CommandsController : Controller
    {
        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok();
        }
    }
}