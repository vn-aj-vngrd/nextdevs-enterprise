using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Interfaces.Repositories;
using Backend.Application.Wrappers;
using Backend.Domain.Products.DTOs;
using MediatR;

namespace Backend.Application.Features.Products.Queries.GetPagedListProduct;

public class GetPagedListProductQueryHandler(IProductRepository productRepository)
    : IRequestHandler<GetPagedListProductQuery, PagedResponse<ProductDto>>
{
    public async Task<PagedResponse<ProductDto>> Handle(GetPagedListProductQuery request,
        CancellationToken cancellationToken)
    {
        return await productRepository.GetPagedListAsync(request.PageNumber, request.PageSize, request.SortCriteria,
            request.Filters);
    }
}