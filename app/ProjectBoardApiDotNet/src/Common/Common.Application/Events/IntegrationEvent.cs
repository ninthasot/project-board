using Common.Application.Abstractions;

namespace Common.Application.Events;

public abstract record IntegrationEvent : IIntegrationEvent
{
    public Guid EventId { get; init; }
    public DateTimeOffset OccurredOn { get; init; }

    protected IntegrationEvent()
    {
        EventId = Guid.NewGuid();
        OccurredOn = DateTimeOffset.UtcNow;
    }

    protected IntegrationEvent(Guid eventId, DateTimeOffset occurredOn)
    {
        EventId = eventId;
        OccurredOn = occurredOn;
    }
}
