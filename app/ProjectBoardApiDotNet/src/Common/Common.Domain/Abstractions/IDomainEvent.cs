using MediatR;

namespace Common.Domain.Abstractions;

public interface IDomainEvent : INotification
{
    Guid Id { get; }
    DateTimeOffset OccurredOn { get; }
}
