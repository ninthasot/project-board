using System.Diagnostics;
using System.Security.Cryptography;
using Api.Controllers.Base;
using Boards.Application.DTO;
using Boards.Application.Queries.GetBoardById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class BoardController : BaseController
{
    public BoardController(IMediator mediator)
        : base(mediator) { }

    [HttpGet("ping")]
    public IActionResult Ping()
    {
        using var activity = new Activity("PingActivity");
        activity.Start();
        int secureNumber = RandomNumberGenerator.GetInt32(0, 100); // 0 <= number < 100
        activity.Stop();
        return Ok(secureNumber);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<BoardDto>> GetBoardById(Guid id)
    {
        var result = await Mediator.Send(new GetBoardByIdQuery(id));

        return ToOkActionResult(result);
    }
}
