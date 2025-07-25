﻿using Common.Constants;
using Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure;

public sealed class ProjectIdentityDbContext : IdentityDbContext<ApplicationUser>
{
    public ProjectIdentityDbContext(DbContextOptions<ProjectIdentityDbContext> options)
        : base(options) { }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.HasDefaultSchema(DatabaseSchema.Identity);
    }
}
