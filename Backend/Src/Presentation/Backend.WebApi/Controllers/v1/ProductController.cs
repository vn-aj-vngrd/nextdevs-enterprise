using System.Threading.Tasks;
using Backend.Application.Features.Products.Commands.CreateProduct;
using Backend.Application.Features.Products.Commands.DeleteProduct;
using Backend.Application.Features.Products.Commands.UpdateProduct;
using Backend.Application.Features.Products.Queries.GetPagedListProduct;
using Backend.Application.Features.Products.Queries.GetProductById;
using Backend.Application.Wrappers;
using Backend.Domain.Products.DTOs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Backend.WebApi.Controllers.v1;

[ApiVersion("1")]
public class ProductController : BaseApiController
{
    [HttpGet]
    [Authorize]
    public async Task<PagedResponse<ProductDto>> GetPagedListProduct([FromQuery] GetPagedListProductQuery model)
    {
        return await Mediator.Send(model);
    }

    [HttpGet]
    public async Task<BaseResult<ProductDto>> GetProductById([FromQuery] GetProductByIdQuery model)
    {
        return await Mediator.Send(model);
    }

    [HttpPost]
    [Authorize]
    public async Task<BaseResult<long>> CreateProduct(CreateProductCommand model)
    {
        return await Mediator.Send(model);
    }

    [HttpPut]
    [Authorize]
    public async Task<BaseResult> UpdateProduct(UpdateProductCommand model)
    {
        return await Mediator.Send(model);
    }

    [HttpDelete]
    [Authorize]
    public async Task<BaseResult> DeleteProduct([FromQuery] DeleteProductCommand model)
    {
        return await Mediator.Send(model);
    }
}