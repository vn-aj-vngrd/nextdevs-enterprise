using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Interfaces;
using Backend.Domain.Products.Entities;
using Backend.Infrastructure.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence.Contexts;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IAuthenticatedUserService authenticatedUser) : DbContext(options)
{
    public DbSet<Product> Products { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
    {
        ChangeTracker.ApplyAuditing(authenticatedUser);

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        this.ConfigureDecimalProperties(builder);

        base.OnModelCreating(builder);
    }
}