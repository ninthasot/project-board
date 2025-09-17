namespace Boards.Domain.Events;

public sealed record BoardCreatedEvent(
    Guid BoardId,
    string Title,
    string Description,
    Guid CreatedBy
) : DomainEvent;
