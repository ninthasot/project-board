using Common.Domain.Abstractions;

namespace Common.Domain.Events;

public abstract record DomainEvent(Guid Id, DateTimeOffset OccurredOnUtc) : IDomainEvent
{
    protected DomainEvent()
        : this(Guid.NewGuid(), DateTimeOffset.UtcNow) { }
}
