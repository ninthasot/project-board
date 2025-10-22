using Boards.Domain.Events;
using Common.Application.Abstractions;
using Common.Contracts.Events.Boards;
using MediatR;

namespace Boards.Application.DomainEventHandlers;

internal sealed class BoardCreatedDomainEventHandler : INotificationHandler<BoardCreatedDomainEvent>
{
    private readonly IMessageBus _messageBus;

    public BoardCreatedDomainEventHandler(IMessageBus messageBus)
    {
        _messageBus = messageBus;
    }

    public async Task Handle(
        BoardCreatedDomainEvent domainEvent,
        CancellationToken cancellationToken
    )
    {
        var integrationEvent = new BoardCreatedIntegrationEvent(
            domainEvent.BoardId,
            domainEvent.Title,
            domainEvent.Description,
            domainEvent.CreatedBy
        );

        await _messageBus.PublishAsync(integrationEvent, cancellationToken);
    }
}
