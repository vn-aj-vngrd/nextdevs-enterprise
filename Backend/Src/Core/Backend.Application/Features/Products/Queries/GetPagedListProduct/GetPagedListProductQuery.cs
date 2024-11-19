using Backend.Application.Parameters;
using Backend.Application.Wrappers;
using Backend.Domain.Products.DTOs;
using MediatR;

namespace Backend.Application.Features.Products.Queries.GetPagedListProduct;

public class GetPagedListProductQuery : PaginationRequestParameter, IRequest<PagedResponse<ProductDto>>
{
    public string Name { get; set; }
}