namespace Boards.Domain.Events;

public sealed record BoardUpdatedEvent(
    Guid BoardId,
    string Title,
    string Description,
    Guid UpdatedBy
) : DomainEvent;
