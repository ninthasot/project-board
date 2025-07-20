using Boards.Application.Commands.CreateBoard;
using Boards.Application.Dtos;
using Boards.Application.Queries.GetBoardById;
using Common.Presentation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Boards.Presentation;

[ApiController]
[Route("api/[controller]")]
public class BoardsController(ISender sender) : BaseController(sender)
{
    [HttpGet("{id}")]
    public async Task<ActionResult<BoardDto>> GetBoardById(Guid id)
    {
        var result = await Sender.Send(new GetBoardByIdQuery(id));

        return ToOkActionResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateBoard([FromBody] CreateBoardDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var command = new CreateBoardCommand(dto.Title, dto.Description);

        var result = await Sender.Send(command);

        return ToCreatedAtActionResult(result, nameof(GetBoardById));
    }
}
