using Boards.Application.Dtos;

namespace Boards.Application.Queries.GetBoardById;

public sealed record GetBoardByIdQuery : IRequest<Result<BoardDto>>
{
    public Guid Id { get; init; }

    public GetBoardByIdQuery(Guid id)
    {
        Id = id;
    }
}
