using Common.Application.Abstractions;
using Common.Application.Events;
using Common.Contracts;
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
        var notification = new IntegrationEventNotification<TIntegrationEvent>(integrationEvent);
        await _publisher.Publish(notification, cancellationToken);
    }
}
