using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Users.Infrastructure.Persistence.Configurations;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(u => u.Id);

        builder.Property(u => u.UserName).HasMaxLength(100);

        builder.Property(u => u.Email).HasMaxLength(256);

        builder.Property(u => u.DisplayName).IsRequired().HasMaxLength(200);

        builder.Property(u => u.AvatarUrl).HasMaxLength(500);

        builder.Property(u => u.Bio).HasMaxLength(1000);

        builder.Property(u => u.IsActive).IsRequired();

        // Index for fast lookup
        builder.HasIndex(u => u.Email);
        builder.HasIndex(u => u.UserName);
        builder.HasIndex(u => u.DisplayName);
    }
}
