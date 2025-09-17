namespace Common.Domain.Abstractions;

public interface IAggregateRoot
{
    IReadOnlyList<IDomainEvent> DomainEvents { get; }
    void RaiseDomainEvent(IDomainEvent domainEvent);
    void ClearDomainEvents();
}
