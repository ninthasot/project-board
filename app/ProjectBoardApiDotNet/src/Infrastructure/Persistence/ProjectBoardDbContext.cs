using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence;

public sealed class ProjectBoardDbContext(DbContextOptions<ProjectBoardDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseSchema.ProjectBoard);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectBoardDbContext).Assembly);
    }
}