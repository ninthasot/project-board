using Common.Contracts;

namespace Common.Application.Abstractions;

public interface IMessageBus
{
    Task PublishAsync<TIntegrationEvent>(
        TIntegrationEvent integrationEvent,
        CancellationToken cancellationToken = default
    )
        where TIntegrationEvent : IIntegrationEvent;
}
