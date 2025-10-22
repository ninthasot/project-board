namespace Common.Contracts;

public abstract record IntegrationEvent : IIntegrationEvent
{
    public Guid EventId { get; init; }
    public DateTimeOffset OccurredOn { get; init; }

    protected IntegrationEvent()
    {
        EventId = Guid.NewGuid();
        OccurredOn = DateTimeOffset.UtcNow;
    }
}
