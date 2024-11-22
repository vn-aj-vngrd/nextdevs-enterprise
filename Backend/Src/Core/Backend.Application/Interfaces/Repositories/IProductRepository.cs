using System.Collections.Generic;
using System.Threading.Tasks;
using Backend.Application.DTOs;
using Backend.Application.Parameters;
using Backend.Domain.Products.DTOs;
using Backend.Domain.Products.Entities;

namespace Backend.Application.Interfaces.Repositories;

public interface IProductRepository : IGenericRepository<Product>
{
    Task<PaginationResponseDto<ProductDto>> GetPagedListAsync(string Name, int pageNumber, int pageSize,
        List<SortCriterion<ProductDto>> sortCriteria, List<FilterCriterion<ProductDto>> filters);
}