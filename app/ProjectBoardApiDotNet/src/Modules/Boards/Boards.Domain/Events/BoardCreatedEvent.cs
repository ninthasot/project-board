using Common.Domain.Events;

namespace Boards.Domain.Events;

public sealed record BoardCreatedEvent(
    Guid BoardId,
    string Title,
    string Description,
    string CreatedBy
) : DomainEvent;
