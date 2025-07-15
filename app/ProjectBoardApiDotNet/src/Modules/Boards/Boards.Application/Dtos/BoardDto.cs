namespace Boards.Application.Dtos;

public sealed record BoardDto
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public DateTimeOffset CreatedAtUtc { get; set; }
}
