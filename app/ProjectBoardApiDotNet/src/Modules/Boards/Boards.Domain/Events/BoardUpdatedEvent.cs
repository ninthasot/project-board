using Common.Domain.Events;

namespace Boards.Domain.Events;

public sealed record BoardUpdatedEvent(
    Guid BoardId,
    string Title,
    string Description,
    string UpdatedBy
) : DomainEvent;
