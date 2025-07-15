namespace Boards.Application.Dtos;

public sealed record CreateBoardDto
{
    public required string Title { get; init; }
    public required string Description { get; init; }
}
