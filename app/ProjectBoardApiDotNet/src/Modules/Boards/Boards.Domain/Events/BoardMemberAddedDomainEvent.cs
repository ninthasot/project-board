namespace Boards.Domain.Events;

public sealed record BoardMemberAddedDomainEvent(Guid BoardId, Guid MemberId, int Role)
    : DomainEvent;
