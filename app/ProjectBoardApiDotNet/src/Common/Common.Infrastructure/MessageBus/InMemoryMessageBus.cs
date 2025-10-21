using Common.Application.Abstractions;
using MediatR;

namespace Common.Infrastructure.MessageBus;

internal sealed class InMemoryMessageBus : IMessageBus
{
    private readonly IPublisher _publisher;

    public InMemoryMessageBus(IPublisher publisher)
    {
        _publisher = publisher;
    }

    public async Task PublishAsync<TIntegrationEvent>(
        TIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default
    )
        where TIntegrationEvent : IIntegrationEvent
    {
        // In a modular monolith, we can use MediatR to publish integration events
        // as INotification across modules
        await _publisher.Publish(integrationEvent, cancellationToken);
    }
}
