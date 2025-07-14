namespace Boards.Infrastructure.Repositories;

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
}
