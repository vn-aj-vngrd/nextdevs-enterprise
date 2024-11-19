using System;
using Backend.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Identity.Contexts;

public class IdentityContext(DbContextOptions<IdentityContext> options)
    : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.HasDefaultSchema("Identity");
        builder.Entity<ApplicationUser>(entity => { entity.ToTable("User"); });

        builder.Entity<ApplicationRole>(entity => { entity.ToTable("Role"); });
        builder.Entity<IdentityUserRole<Guid>>(entity => { entity.ToTable("UserRoles"); });

        builder.Entity<IdentityUserClaim<Guid>>(entity => { entity.ToTable("UserClaims"); });

        builder.Entity<IdentityUserLogin<Guid>>(entity => { entity.ToTable("UserLogins"); });

        builder.Entity<IdentityRoleClaim<Guid>>(entity => { entity.ToTable("RoleClaims"); });

        builder.Entity<IdentityUserToken<Guid>>(entity => { entity.ToTable("UserTokens"); });
    }
}