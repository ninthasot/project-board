using Common.Contracts;
using MediatR;

namespace Common.Application.Events;

public sealed record IntegrationEventNotification<TEvent>(TEvent Event) : INotification
    where TEvent : IIntegrationEvent;
