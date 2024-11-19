using System.Threading.Tasks;
using Backend.Application.DTOs;
using Backend.Domain.Products.DTOs;
using Backend.Domain.Products.Entities;

namespace Backend.Application.Interfaces.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<PaginationResponseDto<ProductDto>> GetPagedListAsync(int pageNumber, int pageSize, string name);
}