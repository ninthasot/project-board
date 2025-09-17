namespace Boards.Domain.Events;

public sealed record BoardMemberAddedEvent(Guid BoardId, Guid MemberId, int Role) : DomainEvent;
