using FluentResults;
using MediatR;

namespace Common.Application.Messaging;

public interface IQuery<TResponse> : IRequest<Result<TResponse>>;
