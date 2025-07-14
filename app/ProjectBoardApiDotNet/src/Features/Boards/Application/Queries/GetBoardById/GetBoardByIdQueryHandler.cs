namespace Boards.Application.Queries.GetBoardById;

public sealed record GetBoardByIdQueryHandler : IRequestHandler<GetBoardByIdQuery, Result<BoardDto>>
{
    private readonly IBoardRepository _boardRepository;

    public GetBoardByIdQueryHandler(IBoardRepository boardRepository)
    {
        _boardRepository = boardRepository;
    }

    public async Task<Result<BoardDto>> Handle(
        GetBoardByIdQuery request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        var board = await _boardRepository.GetByIdAsync(request.Id, cancellationToken);

        if (board is null)
            return Result.Fail(SharedErrorFactory.NotFound);

        var dto = new BoardDto
        {
            Id = board.Id,
            Title = board.Title,
            Description = board.Description,
            CreatedAtUtc = board.CreatedAtUtc,
        };

        return Result.Ok(dto);
    }
}
