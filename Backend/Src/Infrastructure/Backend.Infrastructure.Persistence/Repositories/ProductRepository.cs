using System.Linq;
using System.Threading.Tasks;
using Backend.Application.DTOs;
using Backend.Application.Interfaces.Repositories;
using Backend.Domain.Products.DTOs;
using Backend.Domain.Products.Entities;
using Backend.Infrastructure.Persistence.Contexts;

namespace Backend.Infrastructure.Persistence.Repositories;

public class ProductRepository(ApplicationDbContext dbContext)
    : GenericRepository<Product>(dbContext), IProductRepository
{
    public async Task<PaginationResponseDto<ProductDto>> GetPagedListAsync(int pageNumber, int pageSize, string name)
    {
        var query = dbContext.Products.OrderBy(p => p.Created).AsQueryable();

        if (!string.IsNullOrEmpty(name)) query = query.Where(p => p.Name.Contains(name));

        return await Paged(
            query.Select(p => new ProductDto(p)),
            pageNumber,
            pageSize);
    }
}