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
                new("Product 5", 200000, "555555555555"),
                new("Product 6", 250000, "666666666666"),
                new("Product 7", 300000, "777777777777"),
                new("Product 8", 350000, "888888888888"),
                new("Product 9", 400000, "999999999999"),
                new("Product 10", 450000, "000000000000"),
                new("Product 11", 500000, "101010101010"),
                new("Product 12", 550000, "202020202020"),
                new("Product 13", 600000, "303030303030"),
                new("Product 14", 650000, "404040404040"),
                new("Product 15", 700000, "505050505050"),
                new("Product 16", 750000, "606060606060"),
                new("Product 17", 800000, "707070707070"),
                new("Product 18", 850000, "808080808080"),
                new("Product 19", 900000, "909090909090"),
                new("Product 20", 950000, "010101010101")
            ];

            await applicationDbContext.Products.AddRangeAsync(defaultProducts);

            await applicationDbContext.SaveChangesAsync();
        }
    }
}