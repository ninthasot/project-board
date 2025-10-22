namespace Boards.Domain.Events;

public sealed record ColumnAddedToBoardDomainEvent(
    Guid BoardId,
    Guid ColumnId,
    string ColumnTitle,
    int Position
) : DomainEvent;
