using FluentResults;
using MediatR;

namespace Common.Application.Messaging;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;