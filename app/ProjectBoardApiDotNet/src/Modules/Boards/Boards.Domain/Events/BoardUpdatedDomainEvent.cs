using Common.Domain.Events;

namespace Boards.Domain.Events;

public sealed record BoardUpdatedDomainEvent(
    Guid BoardId,
    string Title,
    string Description,
    string UpdatedBy
) : DomainEvent;
