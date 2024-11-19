using Backend.Application.Wrappers;
using MediatR;

namespace Backend.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommand : IRequest<BaseResult>
{
    public long Id { get; set; }
}