using Common.Application.Messaging;
using Common.Domain.Events;

namespace Boards.Application.Commands.CreateBoard;

public sealed record CreateBoardCommand(string Title, string Description) : ICommand<Guid>;
