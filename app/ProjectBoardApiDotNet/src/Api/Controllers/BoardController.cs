using System.Diagnostics;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BoardController(ILogger<BoardController> logger) : ControllerBase
{
    [HttpGet("ping")]
    public IActionResult Ping()
    {
        using var activity = new Activity("PingActivity");
        activity.Start();
        logger.LogDebug("in ping endpoint");
        int secureNumber = RandomNumberGenerator.GetInt32(0, 100); // 0 <= number < 100
        activity.Stop();
        return Ok(secureNumber);
    }
}
