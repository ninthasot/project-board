namespace Infrastructure.Persistence;

public sealed class ProjectBoardDbContext(DbContextOptions<ProjectBoardDbContext> options)
    : DbContext(options)
{
    public DbSet<Attachment> Attachments { get; set; }
    public DbSet<Board> Boards { get; set; }
    public DbSet<BoardMember> BoardMembers { get; set; }
    public DbSet<Card> Cards { get; set; }
    public DbSet<CardLabel> CardLabels { get; set; }
    public DbSet<Column> Columns { get; set; }
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Label> Labels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseSchema.ProjectBoard);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectBoardDbContext).Assembly);
    }
}
