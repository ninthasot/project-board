using Boards.Domain.Abstractions;

namespace Boards.Infrastructure.Persistence.Repositories;

public sealed class BoardRepository : IBoardRepository
{
    private readonly BoardDbContext _context;

    public BoardRepository(BoardDbContext context)
    {
        _context = context;
    }

    public async Task<Board?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _context
            .Boards.AsNoTracking()
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);

        return response;
    }

    public Task<Board> AddAsync(Board entity, CancellationToken cancellationToken = default)
    {
        _context.Boards.Add(entity);
        return Task.FromResult(entity);
    }
}
