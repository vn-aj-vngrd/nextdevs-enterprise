using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Domain.Products.Entities;
using Backend.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Backend.Infrastructure.Persistence.Seeds;

public static class DefaultData
{
    public static async Task SeedAsync(ApplicationDbContext applicationDbContext)
    {
        if (!await applicationDbContext.Products.AnyAsync())
        {
            List<Product> defaultProducts =
            [
                new("Product 1", 100000, "111111111111"),
                new("Product 2", 150000, "222222222222"),
                new("Product 3", 200000, "333333333333"),
                new("Product 4", 105000, "444444444444"),
                new("Product 5", 200000, "555555555555")
            ];

            await applicationDbContext.Products.AddRangeAsync(defaultProducts);

            await applicationDbContext.SaveChangesAsync();
        }
    }
}