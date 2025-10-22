using Boards.Domain.Entities;
using Common.Application.Abstractions;
using Common.Application.Messaging;

namespace Boards.Application.Commands.CreateBoard;

public sealed class CreateBoardCommandHandler : ICommandHandler<CreateBoardCommand, Guid>
{
    private readonly IBoardRepository _boardRepository;
    private readonly IBoardsUnitOfWork _boardsUnitOfWork;

    public CreateBoardCommandHandler(IBoardRepository boardRepository, IBoardsUnitOfWork unitOfWork)
    {
        _boardRepository = boardRepository;
        _boardsUnitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(
        CreateBoardCommand request,
        CancellationToken cancellationToken
    )
    {
        ArgumentNullException.ThrowIfNull(request);

        var board = Board.Create(
            request.Title,
            request.Description,
            string.Empty // TODO: Replace with actual user ID
        );

        await _boardRepository.AddAsync(board, cancellationToken);
        await _boardsUnitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Ok(board.Id);
    }
}
