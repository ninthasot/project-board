namespace Boards.Application.Commands.CreateBoard;

public sealed class CreateBoardCommandHandler : IRequestHandler<CreateBoardCommand, Result<Guid>>
{
    private readonly IBoardRepository _boardRepository;

    public CreateBoardCommandHandler(IBoardRepository boardRepository)
    {
        _boardRepository = boardRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateBoardCommand request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);
        // Validation, entity creation, etc.
        var board = new Board()
        {
            Id = Guid.NewGuid(),
            Title = request.Title,
            Description = request.Description,
            CreatedAtUtc = DateTimeOffset.UtcNow,
        };

        await _boardRepository.AddAsync(board, cancellationToken);

        return Result.Ok(board.Id);
    }
}
