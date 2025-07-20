using FluentResults;
using MediatR;

namespace Common.Application.Messaging;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
    where TQuery : IQuery<TResponse>;
