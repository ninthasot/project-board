namespace Boards.Domain.Events;

public sealed record ColumnAddedToBoardEvent(
    Guid BoardId,
    Guid ColumnId,
    string ColumnTitle,
    int Position
) : DomainEvent;
