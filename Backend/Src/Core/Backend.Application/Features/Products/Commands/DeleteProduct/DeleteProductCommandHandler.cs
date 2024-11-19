using System.Threading;
using System.Threading.Tasks;
using Backend.Application.Helpers;
using Backend.Application.Interfaces;
using Backend.Application.Interfaces.Repositories;
using Backend.Application.Wrappers;
using MediatR;

namespace Backend.Application.Features.Products.Commands.DeleteProduct;

public class DeleteProductCommandHandler(
    IProductRepository productRepository,
    IUnitOfWork unitOfWork,
    ITranslator translator) : IRequestHandler<DeleteProductCommand, BaseResult>
{
    public async Task<BaseResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await productRepository.GetByIdAsync(request.Id);

        if (product is null)
            return new Error(ErrorCode.NotFound,
                translator.GetString(TranslatorMessages.ProductMessages.Product_NotFound_with_id(request.Id)),
                nameof(request.Id));

        productRepository.Delete(product);
        await unitOfWork.SaveChangesAsync();

        return BaseResult.Ok();
    }
}