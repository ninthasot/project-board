using Common.Constants;
using Users.Infrastructure.Persistence.Configurations;

namespace Users.Infrastructure.Persistence;

public class UserDbContext(DbContextOptions<UserDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.HasDefaultSchema(DatabaseSchema.User);

        modelBuilder.ApplyConfiguration(new UserConfiguration());
    }
}
