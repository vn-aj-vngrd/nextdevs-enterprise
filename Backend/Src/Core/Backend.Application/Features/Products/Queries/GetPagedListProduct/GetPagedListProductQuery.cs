using Backend.Application.Parameters;
using Backend.Application.Wrappers;
using Backend.Domain.Products.DTOs;
using MediatR;

namespace Backend.Application.Features.Products.Queries.GetPagedListProduct;

public class GetPagedListProductQuery : PaginationSortFilterRequestParameter<ProductDto>,
    IRequest<PagedResponse<ProductDto>>
{
}