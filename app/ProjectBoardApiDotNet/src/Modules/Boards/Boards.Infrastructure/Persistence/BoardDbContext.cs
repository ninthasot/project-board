﻿using Boards.Infrastructure.Persistence.Configurations;
using Common.Constants;

namespace Boards.Infrastructure.Persistence;

public class BoardDbContext(DbContextOptions<BoardDbContext> options) : DbContext(options)
{
    public DbSet<Board> Boards { get; set; }
    public DbSet<BoardMember> BoardMembers { get; set; }
    public DbSet<Column> Columns { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseSchema.Board);

        modelBuilder.ApplyConfiguration(new BoardConfiguration());

        modelBuilder.ApplyConfiguration(new BoardMemberConfiguration());

        modelBuilder.ApplyConfiguration(new ColumnConfiguration());
    }
}
