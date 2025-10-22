using Common.Domain.Events;

namespace Boards.Domain.Events;

public sealed record BoardCreatedDomainEvent(
    Guid BoardId,
    string Title,
    string Description,
    string CreatedBy
) : DomainEvent;
