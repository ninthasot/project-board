using Common.Application.Messaging;

namespace Boards.Application.Commands.CreateBoard;

public sealed record CreateBoardCommand(string Title, string Description) : ICommand<Guid>;
