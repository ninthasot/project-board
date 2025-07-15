namespace Boards.Application.Commands.CreateBoard;

public sealed record CreateBoardCommand(string Title, string Description) : IRequest<Result<Guid>>;
