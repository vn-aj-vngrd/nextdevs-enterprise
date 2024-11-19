using Backend.Application.Wrappers;
using MediatR;

namespace Backend.Application.Features.Products.Commands.CreateProduct;

public class CreateProductCommand : IRequest<BaseResult<long>>
{
    public string Name { get; set; }
    public double Price { get; set; }
    public string BarCode { get; set; }
}