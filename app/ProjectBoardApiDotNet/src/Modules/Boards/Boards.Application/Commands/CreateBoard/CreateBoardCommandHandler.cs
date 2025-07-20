using Boards.Domain.Entities;
using Common.Application.Messaging;

namespace Boards.Application.Commands.CreateBoard;

public sealed class CreateBoardCommandHandler : ICommandHandler<CreateBoardCommand, Guid>
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
