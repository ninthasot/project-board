namespace Boards.Application.DTO;

public sealed record CreateBoardDto
{
    public required string Title { get; init; }
    public required string Description { get; init; }
}
