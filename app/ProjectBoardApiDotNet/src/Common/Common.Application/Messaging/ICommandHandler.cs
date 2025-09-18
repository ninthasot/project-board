using Common.Domain.Events;
using FluentResults;
using MediatR;

namespace Common.Application.Messaging;

public interface ICommandHandler<in TCommand, TResponse>
    : IRequestHandler<TCommand, Result<TResponse>>
    where TCommand : ICommand<TResponse>;