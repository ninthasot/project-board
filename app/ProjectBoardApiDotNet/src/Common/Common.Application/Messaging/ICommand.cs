using FluentResults;
using MediatR;

namespace Common.Domain.Events;

public interface ICommand<TResponse> : IRequest<Result<TResponse>>, IBaseCommand;

public interface IBaseCommand;
