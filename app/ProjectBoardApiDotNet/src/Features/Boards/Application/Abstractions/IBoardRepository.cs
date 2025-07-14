namespace Boards.Application.Abstractions;

public interface IBoardRepository
{
    Task<Board?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
}
