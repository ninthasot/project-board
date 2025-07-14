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

    [HttpPost]
    public async Task<ActionResult<Guid>> CreateBoard([FromBody] CreateBoardDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var command = new CreateBoardCommand(dto.Title, dto.Description);

        var result = await Mediator.Send(command);

        return ToCreatedActionResult(result, nameof(GetBoardById), new { id = result.Value });
    }
}
