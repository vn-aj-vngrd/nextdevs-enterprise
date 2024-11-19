using Backend.Application.Wrappers;
using Backend.Domain.Products.DTOs;
using MediatR;

namespace Backend.Application.Features.Products.Queries.GetProductById;

public class GetProductByIdQuery : IRequest<BaseResult<ProductDto>>
{
    public long Id { get; set; }
}